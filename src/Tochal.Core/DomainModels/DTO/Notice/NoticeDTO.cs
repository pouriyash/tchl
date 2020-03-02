using System; 

namespace Tochal.Core.DTO.Notice
{
    public class NoticeDTO
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

        public string CreateDateFriendly { get; set; }
    }
}
