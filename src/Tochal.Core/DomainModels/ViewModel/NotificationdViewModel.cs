using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public string Receiver { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? CreatorUserId { get; set; }
        public int? ToUserId { get; set; }
        public DateTime? ScheduleDate { get; set; } = DateTime.Now;

    }

    public class NotificationSearchViewModel
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ShowDate { get; set; }
    }
}
