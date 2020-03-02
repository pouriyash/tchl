using Tochal.Core.DomainModels.Entity.Identity;
using System; 

namespace Tochal.Core.DTO.Notice
{
    public partial class NoticeFullDTO
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreatorUserId { get; set; }
        public int? ToUserId { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Module { get; set; }
        public int? EntityId { get; set; }

        public virtual User CreatorUser { get; set; }
        public virtual User ToUser { get; set; }
    }
}
