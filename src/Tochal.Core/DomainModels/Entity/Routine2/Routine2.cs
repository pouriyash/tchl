﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Routine2
    {
        public int Id { get; set; }


        /// <summary>
        /// اسم روال
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string Title { get; set; }


        /// <summary>
        /// اسم جدول در دیتابیس
        /// </summary>
        [MaxLength(1024)]
        public string TableName { get; set; }


        /// <summary>
        /// اسم ستون PK
        /// </summary>
        [MaxLength(1024)]
        public string PkColumnName { get; set; }


        public virtual ICollection<Routine2Role> Roles { get; set; }
        public virtual ICollection<Routine2Step> Steps { get; set; }
        public virtual ICollection<Routine2Action> Actions { get; set; }
        public virtual ICollection<Routine2Autodash> Autodashes { get; set; }
        public virtual ICollection<Routine2Reminder> Reminders { get; set; }
        public virtual ICollection<Routine2Notice> Notices { get; set; }
    }
}
