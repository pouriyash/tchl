using Tochal.Core.DomainModels.Entity.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Notice
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatorUserId { get; set; }
        public int? ToUserId { get; set; }
        public bool IsRead { get; set; }
        [MaxLength(512)]
        public string Title { get; set; }
        public string Body { get; set; }
        [MaxLength(2048)]
        public string Module { get; set; }
        public int? EntityId { get; set; }

        [ForeignKey("CreatorUserId")]
        //[InverseProperty("NoticeCreatorUser")]
        public virtual User CreatorUser { get; set; }

        [ForeignKey("ToUserId")]
        //[InverseProperty("NoticeToUser")]
        public virtual User ToUser { get; set; }
    }
}
