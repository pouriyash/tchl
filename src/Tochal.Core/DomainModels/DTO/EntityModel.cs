using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.DTO
{
    public class EntityModel
    {
    }

    public class PagingModel
    {
        public int pageSize { get; set; } = 10;

        public int pageNumber { get; set; } = 1;

        public int totalItemCount { get; set; } = 0;
    }
}
