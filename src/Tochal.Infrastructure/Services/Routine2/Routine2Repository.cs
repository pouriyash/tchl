using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Alamut.Data.Structure;
using Alamut.Service.Messaging.Contracts;
using Alamut.Utilities.Http;
using AutoMapper;

using Dapper;
using DNTPersianUtils.Core;
using Tochal.Core.Common.Extension;
using Tochal.Core.Common.IdentityToolkit;
using Tochal.Core.DomainModels.DTO.Routine2;
using Tochal.Core.DomainModels.DTO.User;

using Tochal.Core.DomainModels.SSOT;
using Tochal.Core.DomainModels.ViewModel.Routine2;
using Tochal.Infrastructure.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Messaging.Configration;
using Service.Messaging.Contracts;
using Tochal.Core.DomainModels.Entity.Routine2;
using AutoMapper.QueryableExtensions;


/*
 * Developed By: Mojtba Dashtinejad
 * لطفا بدون هماهنگی بر روی این فایل هیچ گونه تغییری ایجاد نکنید
 * نه کدی را پاک کنید، نه ویرایش کنید و نه حذف کنید
 */
namespace Tochal.Infrastructure.Services.Routine2
{
    public class Routine2Repository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _connection;
        private readonly IUserResolverService _userResolverService;

        //private readonly ISmsService _smsService;
        //private readonly IEmailService _emailService;

        //private readonly IUserResolverService _userResolverService;


        public Routine2Repository(ApplicationDbContext context
            , IDbConnection connection
            ,IUserResolverService userResolverService
//, ISmsService smsService
//, IEmailService emailService
/*            ,IUserResolverService userResolverService*/)
        {
            _userResolverService = userResolverService;
            _connection = connection;
            _context = context;
            //_smsService = smsService;
            //_emailService = emailService;
            //_userResolverService = userResolverService;
        }

        public Routine2Step GetStep(int routineId, int currentStep)
        {
            var step = _context.Routine2Step
                .AsNoTracking()
                .FirstOrDefault(q => q.RoutineId == routineId && q.Step == currentStep);

            return step;
        }

        // این متد مراحل یک کارتابل را برای روالی خاص برمی‌گرداند
        public List<int> GetRoleSteps(int routineId, string dashboardEnum)
        {
            var role = _context.Routine2Role
                .FirstOrDefault(q => q.RoutineId == routineId && q.DashboardEnum == dashboardEnum);

            if (role == null)
                throw new Exception($"داشبوردی با عنوان {dashboardEnum} در روال {routineId} یافت نشد");

            return JsonConvert.DeserializeObject<List<int>>(role.StepsJson);
        }

        public List<int> GetUserEntityIds(int routineId, int userId, string dashboardEnum)
        {
            var role = _context.Routine2Role
                .FirstOrDefault(q => q.RoutineId == routineId && q.DashboardEnum == dashboardEnum);

            var model = _context.Routine2Log
                .Where(l => l.RoutineId == routineId &&
                            l.UserId == userId &&
                            l.RoutineRoleTitle == role.Title)
                .Select(l => l.EntityId).ToList();

            return model;
        }

        public List<Routine2Step> GetRoutineStepsByRoutineId(int routineId)
        {
            var model = _context.Routine2Step
                .Where(q => q.RoutineId == routineId)
                .OrderBy(x => x.Step)
                .ToList();

            return model;
        }

        public ServiceResult<int> CreateLog(CreateRoutine2LogViewModel vm)
        {
            // map the ViewModel to a new entity
            
            var entity = Mapper.Map<Routine2Log>(vm);

            // Add it to database
            _context.Routine2Log.Add(entity);
            _context.SaveChanges();

            // return the new id
            return ServiceResult<int>.Okay(entity.Id);
        }

        public string GetRoutineRoleTitle(int routineId, int step)
        {
            var routineRole = _context.Routine2Role
                .FirstOrDefault(q => q.RoutineId == routineId && q.StepsJson.Contains($"\"{step}\""));

            return routineRole?.Title;
        }

        public Routine2FullDTO GetRoutineFull(int routineId)
        {
            var model = _context.Routine2
                .AsNoTracking()
                .ProjectTo<Routine2FullDTO>()
                .FirstOrDefault(q => q.Id == routineId);

            return model;
        }

        public List<Routine2Log> GetLogs(int routineId, int entityId)
        {
            return _context.Routine2Log
                .Where(q => q.RoutineId == routineId &&
                            q.EntityId == entityId) 
                .OrderByDescending(q => q.ActionDate)
                .ToList();
        }

        #pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<Routine2NoticeOutput> SendNoticeAsync(
        #pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
              int routineId
            , int entityId
            , string userIdsSqlQuery
            , string modelSqlQuery
            , string body
            , string bodySms
            , string bodyEmail
            , bool realSend = true)
        {
            var noticeOutput = new Routine2NoticeOutput();
            
            // پیغام را باید به چه کسانی بفرستیم؟
            var userIds = _connection.Query<int>(userIdsSqlQuery, new { EntityId = entityId }).ToList();

            // اگر هیچ کاربری پیدا نشد، سراغ پیغام بعدی میرویم
            if (userIds.Count == 0)
            {
                return noticeOutput;
            }

            noticeOutput.UserIds = userIds;

            // پیغامی که باید برای تک تک کاربران ارسال شود را میسازیم
            foreach (var userId in userIds)
            {
                var userNoticeOutput = new Routine2NoticeSpecificUser { UserId = userId };

                // اگر مدل باید بگیریم، کوئری را اجرا میکنیم
                body = body ?? "";
                bodySms = bodySms ?? "";
                bodyEmail = bodyEmail ?? "";

                if (!string.IsNullOrWhiteSpace(modelSqlQuery))
                {
                    var model = _connection.QueryFirstOrDefault(modelSqlQuery,
                        new { EntityId = entityId, UserId = userId });

                    if (model != null)
                    {
                        // مدل را تبدیل به دیکشنری میکنیم
                        var jsonString = JsonConvert.SerializeObject(model);
                        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

                        userNoticeOutput.Model = dictionary;

                        // convert dictionary to dynamic
                        var eo = new ExpandoObject();
                        var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
                        foreach (var kvp in dictionary)
                        {
                            eoColl.Add(kvp);
                        }

                        dynamic dynamicModel = eo;

                        if (!string.IsNullOrWhiteSpace(body))
                            body = StringExtensions.CompileRazor(body, dynamicModel);

                        if (!string.IsNullOrWhiteSpace(bodySms))
                            bodySms = StringExtensions.CompileRazor(bodySms, dynamicModel);

                        if (!string.IsNullOrWhiteSpace(bodyEmail))
                            bodyEmail = StringExtensions.CompileRazor(bodyEmail, dynamicModel);
                    }
                }

                userNoticeOutput.BodyCompiled = body;
                userNoticeOutput.BodySmsCompiled = bodySms;
                userNoticeOutput.BodyEmailCompiled = bodyEmail;

                if (!string.IsNullOrWhiteSpace(body))
                {
                    try
                    {
                        // پیغام را میفرستیم
                        _context.Notice2.Add(new Notice2
                        {
                            CreateDate = DateTime.Now,
                            EntityId = entityId,
                            RoutineId = routineId,
                            Body = body,
                            IsRead = false,
                            CreatorUserId = _userResolverService.GetUserId(),
                            ToUserId = userId,
                        });

                        _context.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }


                if (!string.IsNullOrWhiteSpace(bodySms) || !string.IsNullOrWhiteSpace(bodyEmail))
                {
                    // برای ارسال اس ام اس و ایمیل باید کاربر را از دیتابیس بگیریم
                    var targetUser = _connection.QuerySingle<UserSummary>("SELECT * FROM [Users] WHERE [Id] = @UserId",
                        new { UserId = userId });

                    userNoticeOutput.User = targetUser;

                    if (!string.IsNullOrWhiteSpace(bodySms))
                    {
                        // ارسال SMS
                        try
                        {
                            // اگر موبایل درست بود
                            if (targetUser.PhoneNumber.IsValidIranianMobileNumber())
                            {
                                // اس ام اس را ارسال میکنیم
                                if (realSend)
                                {
                                    #pragma warning disable 4014
                                    //_smsService.SendSmsAsync(targetUser.PhoneNumber, bodySms);
                                    #pragma warning restore 4014
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(bodyEmail))
                    {
                        // ارسال ایمیل
                        try
                        {
                            // اگر ایمیل درست بود
                            if (targetUser.Email.IsValidEmail())
                            {
                                // ایمیل را ارسال میکنیم
                                if (realSend)
                                {
                                    //_emailService.SendEmail(new EmailBody<int>
                                    //{
                                    //    To = targetUser.Email,
                                    //    Subject = "ارسال ایمیل از طریق سامانه شهر پژوهش : " + DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds,
                                    //    Body = "<div dir='rtl'>" + bodyEmail + "</div>"
                                    //});
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                noticeOutput.Notices.Add(userNoticeOutput);
            }

            return noticeOutput;
        }

        public async Task<Routine2NoticeOutput> SendNoticeAsync(Routine2Notice noticeTemplate, int entityId, bool realSend = true)
        {
            var noticeOutput = await this.SendNoticeAsync(
                  noticeTemplate.RoutineId
                , entityId
                , noticeTemplate.UserIdsSqlQuery
                , noticeTemplate.ModelSqlQuery
                , noticeTemplate.Body
                , noticeTemplate.BodySms
                , noticeTemplate.BodyEmail
            );

            noticeOutput.Routine2Notice = noticeTemplate;
            return noticeOutput;
        }

        public async Task<Routine2NoticeOutput> SendNoticeAsync(Routine2Reminder reminder, int entityId)
        {
            return await this.SendNoticeAsync(
                  reminder.RoutineId
                , entityId
                , reminder.UserIdsSqlQuery
                , reminder.ModelSqlQuery
                , reminder.Body
                , reminder.BodySms
                , reminder.BodyEmail
            );
        }

        public async Task<List<Routine2NoticeOutput>> SendNoticeAsync(Routine2ChangeStepResult changeStepResult, bool realSend = true)
        {
            // تمپلیت‌های نامه را میگیریم
            var noticeTemplates = _context.Routine2Notice
                .AsNoTracking()
                .Where(q => q.RoutineId == changeStepResult.RoutineId)
                .Where(q => q.FromStep == changeStepResult.FromStep)
                .Where(q => q.ToStep == changeStepResult.ToStep)
                .ToList();

            var output = new List<Routine2NoticeOutput>();

            foreach (var noticeTemplate in noticeTemplates)
            {
                var noticeOutput = await this.SendNoticeAsync(noticeTemplate, changeStepResult.EntityId, realSend);
                output.Add(noticeOutput);
            }

            return output;
        }

        public string GetActionTitle(int routineId, int fromStep, Routine2Actions action)
        {
            try
            {
                var routine2Action = _context.Routine2Action
                    .AsNoTracking()
                    .Where(q => q.RoutineId == routineId)
                    .Where(q => q.Step == fromStep)
                    .Where(q => q.Action == action.ToString())
                    .FirstOrDefault();

                return routine2Action == null
                    ? action.ToString()
                    : routine2Action.Title;
            }
            catch (Exception)
            {
                return action.ToString();
            }
        }


        public void Authodash()
        {
            var autodashes = _context.Routine2Autodash
                .AsNoTracking()
                .Include(q => q.Routine)
                .ToList();

            autodashes.ForEach(autodash =>
            {
                var tableName = autodash.Routine.TableName;
                var pkColumnName = autodash.Routine.PkColumnName;

                // شرطی که بر اساسش باید طرح پیدا کنیم
                var whereCondition = $@"
                        [RoutineStep] = {autodash.Step}
                    AND [RoutineIsFlown] = 1
                    AND GETDATE() >= DATEADD(DAY, {autodash.TimeoutDays}, [RoutineStepChangeDate])
                ";

                // طرح‌هایی که با این شرط مطابقت دارند
                var idsQuery = $@"
                    SELECT [{pkColumnName}]
                    FROM [{tableName}] 
                    WHERE {whereCondition}
                ";

                var ids = _connection.Query<int>(idsQuery).ToList();

                // کوئری به روز رسانی تاریخ و تغییر کارتابل
                var updateQuery = $@"
                    UPDATE [{tableName}]

                    SET
                          [RoutineStep] = {autodash.ToStep}
                        , [RoutineStepChangeDate] = GETDATE()

                    WHERE {whereCondition}
                ";

                _connection.Query(updateQuery);


                // لاگ هم باید بزنیم
                ids.ForEach(id =>
                {
                    this.CreateLog(new CreateRoutine2LogViewModel
                    {
                        RoutineId = autodash.RoutineId,
                        EntityId = id,
                        Action = "تغییر کارتابل",
                        Step = autodash.Step,
                        ToStep = autodash.ToStep,
                        UserId = 0, // robot
                        Description = $"تغییر کارتابل توسط سیستم به علت عدم پاسخدهی در {autodash.TimeoutDays} روز",
                        CreatorUserId = 0,
                        RoutineRoleTitle = "سیستم",
                    });
                });
            });
        }

        public void Reminder()
        {
            var reminders = _context.Routine2Reminder
                .AsNoTracking()
                .Include(q => q.Routine)
                .ToList();

            reminders.ForEach(reminder =>
            {
                var tableName = reminder.Routine.TableName;
                var pkColumnName = reminder.Routine.PkColumnName;

                // شرطی که بر اساسش باید طرح پیدا کنیم
                var whereCondition = $@"
                        [RoutineStep] = {reminder.Step}
                    AND [RoutineIsFlown] = 1
                    AND DATEDIFF(DAY, RoutineStepChangeDate, GETDATE()) = {reminder.TimeoutDays}
                ";

                // طرح‌هایی که با این شرط مطابقت دارند
                var entityIdsQuery = $@"
                    SELECT [{pkColumnName}]
                    FROM [{tableName}] 
                    WHERE {whereCondition}
                ";

                var entityIds = _connection.Query<int>(entityIdsQuery).ToList();

                // به ازای هر طرح باید پیغام بزنیم
                entityIds.ForEach(entityId =>
                {
                    #pragma warning disable 4014
                    this.SendNoticeAsync(reminder, entityId);
                    #pragma warning restore 4014
                });
            });
        }
    }
}
