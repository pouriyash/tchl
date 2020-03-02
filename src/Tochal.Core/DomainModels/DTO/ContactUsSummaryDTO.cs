using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.DTO
{

    public class ContactUsSummaryDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// عنوان
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// موضوع
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// شناسه ایجادکننده
        /// </summary>
        public int CreateAnswerUserId { get; set; }

        /// <summary>
        /// شناسه بروزکننده پاسخ
        /// </summary>
        public int UpdateAnswerUserId { get; set; }

        /// <summary>
        /// پاسخ
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// وضعیت پاسخ
        /// </summary>
        public bool? IsAnswer { get; set; }

        /// <summary>
        /// ایمیل
        /// </summary>
        public string Email { get; set; }

        public DateTime? DateSend { get; set; }
        public DateTime? DateAnswer { get; set; }

        /// <summary>
        /// شرح
        /// </summary>
        public string Description { get; set; }
         
        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }

        public int? userId { get; set; }

        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string PhoneNumber { get; set; }
    }

}
