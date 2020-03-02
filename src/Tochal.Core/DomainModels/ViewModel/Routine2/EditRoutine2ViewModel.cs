using System;

namespace Tochal.Core.DomainModels.ViewModel.Routine2
{
    public class EditRoutine2ViewModel
    {

        public EditRoutine2ViewModel(int routineStep, DateTime? routineFlownDate)
        {
            this.RoutineStep = routineStep;
            this.RoutineFlownDate = routineFlownDate ?? DateTime.Now;
        }

        public int RoutineStep { get; set; }
        public DateTime? RoutineFlownDate { get; set; }

        public bool RoutineIsFlown { get; set; } = true;
        public DateTime? RoutineStepChangeDate { get; set; } = DateTime.Now;
        public bool RoutineIsDone { get; set; } = false;
        public bool RoutineIsSucceeded { get; set; } = false;
    }
}
