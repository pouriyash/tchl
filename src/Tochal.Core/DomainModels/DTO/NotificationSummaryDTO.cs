using Tochal.Core.DomainModels.DTO.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.DTO
{
    public class NotificationSummaryDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime? ShowDate { get; set; }
    }

    public class NotificationUserSummaryDTO
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ShowDate { get; set; }
         
        public UserSummary User { get; set; }
         
        public NotificationSummaryDTO Notification { get; set; }
    }
}
