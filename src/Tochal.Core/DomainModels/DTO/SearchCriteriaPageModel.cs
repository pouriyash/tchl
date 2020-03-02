namespace Tochal.Core.DomainModels.DTO
{
    public class SearchCriteriaPageModel<T, E, C>
    {
        public SearchCriteriaPageModel(T data, E search, C count)
        {
            PageModel = data;
            SearchCriteria = search;
            TotalCount = count;
        }

        public T PageModel { get; set; }
        public E SearchCriteria { get; set; }
        public C TotalCount { get; set; }
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
