﻿using System.Collections.Generic;
using Tochal.Core.DomainModels.Entity.Identity;
using cloudscribe.Web.Pagination;

namespace Tochal.Core.DomainModels.ViewModel.Identity
{
    public class PagedAppLogItemsViewModel
    {
        public PagedAppLogItemsViewModel()
        {
            Paging = new PaginationSettings();
        }

        public string LogLevel { get; set; } = string.Empty;

        public List<AppLogItem> AppLogItems { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}