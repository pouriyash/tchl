using System;
using System.Collections.Generic;
using System.Text;

namespace Tochal.Core.DomainModels.ViewModel
{

    public class SearchCriteriaPageModel<T, E>
    {
        public SearchCriteriaPageModel(T data, E search)
        {
            PageModel = data;
            SearchCriteria = search;
        }

        public T PageModel { get; set; }
        public E SearchCriteria { get; set; }
    }


    public class Letters2ShowIcon
    {
        public Letters2ShowIcon(int routineId, int entityId, int? universityId = null)
        {
            this.RoutineId = routineId;
            this.EntityId = entityId;
            this.UniversityId = universityId;
        }

        public int RoutineId { get; set; }
        public int EntityId { get; set; }
        public int? UniversityId { get; set; }
    }

}
