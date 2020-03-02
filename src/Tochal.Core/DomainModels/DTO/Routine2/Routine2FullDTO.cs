using System.Collections.Generic; 
namespace Tochal.Core.DomainModels.DTO.Routine2
{
    public class Routine2FullDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Routine2RoleDTO> Roles { get; set; }
        public virtual ICollection<Routine2StepDTO> Steps { get; set; }
        public virtual ICollection<Routine2ActionDTO> Actions { get; set; }
    }

    public class Routine2ActionDTO
    {
        public int RoutineId { get; set; }
        public int Step { get; set; }
        public string Action { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public bool IsDescriptionRequired { get; set; }
        public bool ShouldHideDescription { get; set; }
        public bool IsDescriptionMultiline { get; set; }

    }

    public class Routine2StepDTO
    {
        public int RoutineId { get; set; }
        public string Title { get; set; }
        public int Step { get; set; }
        public int? F1 { get; set; }
        public int? F2 { get; set; }
        public int? F3 { get; set; }
        public int? F4 { get; set; }
        public int? F5 { get; set; }
        public int? F6 { get; set; }
        public int? F7 { get; set; }
    }

    public class Routine2RoleDTO
    {
        public int RoutineId { get; set; }
        public string Title { get; set; }
        public string StepsJson { get; set; }
        public string DashboardEnum { get; set; }
        public int SortOrder { get; set; }
    }
}
