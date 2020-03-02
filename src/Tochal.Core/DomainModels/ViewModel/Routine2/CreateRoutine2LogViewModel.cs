using System;


namespace Tochal.Core.DomainModels.ViewModel.Routine2
{
    public class CreateRoutine2LogViewModel
    {
        public int RoutineId { get; set; }
        public string RoutineRoleTitle { get; set; }
        public int EntityId { get; set; }
        public int UserId { get; set; }
        public DateTime ActionDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string Action { get; set; }
        public int? CreatorUserId { get; set; }

        public int Step { get; set; }
        public int? ToStep { get; set; }


    }
}
