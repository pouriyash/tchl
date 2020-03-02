using Tochal.Core.DomainModels.Entity.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Printing;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Tochal.Core.DomainModels.Entity
{
    public class Notification
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Module { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        [MaxLength(50)]
        public string Receiver { get; set; }
        [MaxLength(512)]
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatorUserId { get; set; }
        public int? ToUserId { get; set; }
        [MaxLength(50)]
        public string Response { get; set; }
        public DateTime? LastRetryDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public bool IsSent { get; set; }
        public int? ModuleId { get; set; }
        public int? SubSystemType { get; set; }
        public int? SubSystemId { get; set; }
    }

    public class NotificationUser
    {
        public int Id { get; set; }

        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ShowDate { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [ForeignKey(nameof(NotificationId))]
        public Notification Notification { get; set; }
    }
}
