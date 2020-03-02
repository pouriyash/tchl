using Alamut.Data.Structure;
using Tochal.Core.DomainModels.SSOT;
using Tochal.Core.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tochal.Infrastructure.DataLayer.Context;
using Tochal.Core.DomainModels.ViewModel.Routine2;
using Alamut.Utilities.Http;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.DomainModels.DTO.Routine2;


namespace Tochal.Infrastructure.Services.Routine2
{
    public class GeneralRoutine2Repository<T> : IGeneralRoutin2<T> where T : IRoutine2Entity
    {

        #region Dependencies
        private readonly IUnitOfWork _context;
        private readonly Routine2Repository _routine2Repository;
        private readonly IUserResolverService _userResolverService;
        #endregion

        private DbSet<T> table = null;

        #region constructor
        public GeneralRoutine2Repository(IUnitOfWork context,
            IUserResolverService userResolverService,
            Routine2Repository routine2Repository)
        {
            _context = context;
            table = _context.Set<T>();
            _userResolverService = userResolverService;
            _routine2Repository = routine2Repository;

        }
        #endregion


        #region implementation functios

        public int GetNextStep(Routine2Actions action, T entity, int rouitneId)
        {

            var currentStep = _routine2Repository.GetStep(rouitneId, entity.RoutineStep);

            int? nextStep = null;

            switch (action)
            {
                case Routine2Actions.F1:
                    nextStep = currentStep.F1;
                    break;
                case Routine2Actions.F2:
                    nextStep = currentStep.F2;
                    break;
                case Routine2Actions.F3:
                    nextStep = currentStep.F3;
                    break;
                case Routine2Actions.F4:
                    nextStep = currentStep.F4;
                    break;
                case Routine2Actions.F5:
                    nextStep = currentStep.F5;
                    break;
                case Routine2Actions.F6:
                    nextStep = currentStep.F6;
                    break;
                case Routine2Actions.F7:
                    nextStep = currentStep.F7;
                    break;
            }

            return nextStep ?? -1;
        }

        public async Task<ServiceResult<Routine2ChangeStepResult>> ChangeStep(int RoutineId, int EntityId, Routine2Actions Action, string Description)
        {
            try
            {
                // گرفتن رکورد مورد نظر
                var entity = table.Find(EntityId);


                // درصورتی که رکورد مورد نظر وجود نداشت باید خطا برگردانده شود
                if (entity == null)
                    throw new Exception($"داده‌ای با شناسه {EntityId} یافت نشد");
                

                // مرحله بعدی فرآیند دراین قسمت به دست می‌آید
                var nextStep = GetNextStep(Action, entity, RoutineId);

                // درصورتی که مرحله بعدی -1 باشد باید خطا نمایش داده شود
                if (nextStep == -1)
                    throw new Exception("Something went wrong, next dashboard couldn't be -1");


                //=========================================================
                    int userId = _userResolverService.GetUserId();
                    var fromStep = entity.RoutineStep;
                //=========================================================


                // ثبت لاگ جدید برای این کاربر که رکورد مورد را از یک مرحله به مرحله دیگر انتقال داده است
                _routine2Repository.CreateLog(new CreateRoutine2LogViewModel
                {
                    RoutineId = RoutineId,
                    EntityId = EntityId,
                    Description = Description,
                    Action = _routine2Repository.GetActionTitle(RoutineId, fromStep, Action),
                    Step = fromStep,
                    ToStep = nextStep,
                    UserId = userId,
                    CreatorUserId = userId,
                    RoutineRoleTitle = _routine2Repository.GetRoutineRoleTitle(RoutineId, entity.RoutineStep),
                });


                var routineVm = new EditRoutine2ViewModel(nextStep, entity.RoutineFlownDate);

                // اگر در مرحله آخر باشیم، باید به روال برچست تمام شده بزنیم
                if (entity.DoneSteps.Contains(nextStep))
                    routineVm.RoutineIsDone = true;

                // به صورت پیش‌فرض طرح رد شده است، مگر اینکه در مراحل تایید باشیم
                if (entity.SucceededSteps.Contains(nextStep))
                    routineVm.RoutineIsSucceeded = true;


                entity.RoutineStep = nextStep;
                entity.RoutineFlownDate = DateTime.Now;
                entity.RoutineStepChangeDate = DateTime.Now;

                _context.SaveChanges();

                var changeStepResult = new Routine2ChangeStepResult
                {
                    Action = Action,
                    Description = Description,
                    UserId = userId,
                    ToStep = nextStep,
                    FromStep = fromStep,
                    EntityId = EntityId,
                    RoutineId = RoutineId,
                };

                try { await _routine2Repository.SendNoticeAsync(changeStepResult); } catch (Exception) { }

                return ServiceResult<Routine2ChangeStepResult>.Okay(changeStepResult);
            }
            catch (Exception ex)
            {
                return ServiceResult<Routine2ChangeStepResult>.Exception(ex);
            }
        }




        #endregion


    }
}
