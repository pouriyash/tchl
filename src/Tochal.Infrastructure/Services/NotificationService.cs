using AutoMapper.QueryableExtensions;
using Tochal.Core.DomainModels.DTO; 
using Tochal.Core.DomainModels.Entity; 
using Tochal.Infrastructure.Services.GenericService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Alamut.Data.Structure;
using Tochal.Core.DomainModels.ViewModel;
using System;
using Service.Messaging.Configration;
using Tochal.Infrastructure.Services.Contracts;
using Service.Messaging.Contracts;

namespace Tochal.Infrastructure.Services.Test
{
    public class NotificationService : GenericService<Notification>
    {

        private readonly DataLayer.Context.IUnitOfWork _context;
        private DbSet<Notification> table = null;
        private readonly INotificationRepository _notificationeRepository;
        private readonly IEmailService _emailService;
        #region constructor
        public NotificationService(DataLayer.Context.IUnitOfWork context, IEmailService emailService, INotificationRepository notificationeRepository) : base(context)
        {
            _emailService = emailService;
            _notificationeRepository = notificationeRepository;
            _context = context;
            table = _context.Set<Notification>();
        }
        #endregion
  
        public ServiceResult SaveAndSendMail(NotificationViewModel model, bool send = true)
        {
            var notificationId = _notificationeRepository.AddEmail(model);

            if (!send) { return ServiceResult.Okay("ایمیل ذخیره گردید."); }

            if (model.ScheduleDate > DateTime.Now) { return ServiceResult.Okay("ایمیل ذخیره گردید."); }

            var body = new EmailBody<int>
            {
                Id = notificationId,
                Subject = model.Title,
                Body = model.Body,
                To = model.Receiver,
            };

            try
            {
                _emailService.SendEmail(body, _notificationeRepository.MarkMailSent);
                return ServiceResult.Okay($"ایمیل شما با شناسه : { notificationId} با موفقیت ارسال شد .");
            }
            catch (Exception ex)
            {
                return ServiceResult.Exception(ex);
            }
        }

    }

}
