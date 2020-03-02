using System;
using System.Collections.Generic;
using System.Text;
using Tochal.Core.DomainModels.ViewModel;

namespace Tochal.Infrastructure.Services.Contracts
{
    public interface INotificationRepository
    {
        /// <summary>
        /// add email into notification and return id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddEmail(NotificationViewModel model);

        /// <summary>
        /// mark an notification has been sent
        /// </summary>
        /// <param name="id"></param>
        void MarkMailSent(int id);
    }
}
