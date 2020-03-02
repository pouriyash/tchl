using Tochal.Core.DomainModels.Entity.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Tochal.Core.DomainModels.Entity.Routine2
{
    public class Notice2
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatorUserId { get; set; }
        public int? ToUserId { get; set; }
        public bool IsRead { get; set; }

        
        public string Body { get; set; }

        
        public int? RoutineId { get; set; }

        public int? EntityId { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }

        [ForeignKey("ToUserId")]
        public virtual User ToUser { get; set; }

        [ForeignKey("RoutineId")]
        public virtual Routine2 Routine { get; set; }
    }
}
