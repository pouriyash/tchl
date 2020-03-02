
namespace Tochal.Core.DomainModels.DTO.Routine2
{
    public class Routine2PageModel<T, E, X>
    {
        public Routine2PageModel(T data, E search, X currentDashboard)
        {
            PageModel = data;
            SearchCriteria = search;
            CurrentDashboard = currentDashboard;
        }

        public T PageModel { get; set; }
        public E SearchCriteria { get; set; }
        public X CurrentDashboard { get; set; }
    }
}
