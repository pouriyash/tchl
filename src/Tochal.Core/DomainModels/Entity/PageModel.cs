using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.Entity
{
    public class PageModel
    {
        public long TotalItems { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public long CurrentPage { get; set; } = 1;
        public long MaxPagerItems { get; set; } = 10;
        public bool ShowFirstLast { get; set; } 
        public bool ShowNumbered { get; set; }
        public bool UseReverseIncrement { get; set; }
        public bool SuppressEmptyNextPrev { get; set; }
        public bool SuppressInActiveFirstLast { get; set; }
        public bool RemoveNextPrevLinks { get; set; }
    }
}
