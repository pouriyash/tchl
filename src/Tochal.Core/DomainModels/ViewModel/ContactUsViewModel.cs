using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel
{

    public class ContactUsViewModel
    {

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
        /// شرح
        /// </summary>
        public string Description { get; set; }
         
        public string Email { get; set; }

        /// <summary>
        /// نام و نام خانوادگی
        /// </summary>
        public string FullName { get; set; }

        public int? userId { get; set; }
        public DateTime? DateSend { get; set; } = DateTime.Now;
        public DateTime? DateAnswer { get; set; }
        /// <summary>
        /// شماره موبایل
        /// </summary>
        public string PhoneNumber { get; set; }
    }
    public class ContactUsSearchViewModel
    {
        public string Term { get; set; }
        public int SortOrder { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
