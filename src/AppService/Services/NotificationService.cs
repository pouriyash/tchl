//using System;
//using Alamut.Data.Structure;
//using Nahad.Core.ViewModel.Notification;
//using Nahad.Data.Repositories;
//using Alamut.Service.Messaging.Configration;
//using Service.Messaging.Contracts;

//namespace Tochal.AppService.Services
//{
//    public class NotificationService
//    {
//        private readonly INotificationRepository _notificationeRepository;
//        private readonly IEmailService _emailService;


//        public NotificationService(INotificationRepository notificationeRepository,
//            IEmailService emailService)
//        {
//            _notificationeRepository = notificationeRepository;
//            _emailService = emailService;
//        }

//        public ServiceResult SaveAndSendMail(NotificationViewModel model, bool send = true)
//        {
//            var notificationId = _notificationeRepository.AddEmail(model);

//            if (!send) { return ServiceResult.Okay("ایمیل ذخیره گردید."); }

//            if (model.ScheduleDate > DateTime.Now) { return ServiceResult.Okay("ایمیل ذخیره گردید."); }

//            var body = new EmailBody<int>
//            {
//                Id = notificationId,
//                Subject = model.Title,
//                Body = model.Body,
//                To = model.Receiver,
//            };

//            try
//            {
//                _emailService.SendEmail(body, _notificationeRepository.MarkMailSent);
//                return ServiceResult.Okay($"ایمیل شما با شناسه : { notificationId} با موفقیت ارسال شد .");
//            }
//            catch (Exception ex)
//            {
//                return ServiceResult.Exception(ex);
//            }
//        }


//    }
//}