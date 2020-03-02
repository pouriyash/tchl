using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class UserSearchViewModel
    {
        public string Term { get; set; }

        /// <summary>
        /// حقیقی و حقوقی
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// شرکت یا دانشگاه
        /// </summary>
        public int? UserType { get; set; }

        /// <summary>
        /// مرد یا زن
        /// </summary>
        public bool? Gender { get; set; }

        /// <summary>
        /// تایید شدگان و تایید نشدگان
        /// </summary>
        public bool? IsAccept { get; set; }

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
