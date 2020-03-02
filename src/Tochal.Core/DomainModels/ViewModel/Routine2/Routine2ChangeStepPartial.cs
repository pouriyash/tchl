using Tochal.Core.DomainModels.SSOT;
using System;


namespace Tochal.Core.DomainModels.ViewModel.Routine2
{
    public class Routine2ChangeStep
    {

        public Routine2ChangeStep(int entityId, Routine2Actions action)
        {
            this.EntityId = entityId;
            this.Action = action;
        }

        public int EntityId { get; set; }
        public Routine2Actions Action { get; set; }
    }

    public class Routine2CustomAction<T>
    {

        public Routine2CustomAction(T entity, int step)
        {
            this.Entity = entity;
            this.Step = step;
        }

        public T Entity { get; set; }
        public int Step { get; set; }
    }

    public class Routine2Partial<T, S, C>
    {

        public Routine2Partial(T entity, S searchCriteria, C currentDashboard)
        {
            this.Entity = entity;
            this.SearchCriteria = searchCriteria;
            this.CurrentDashboard = currentDashboard;
        }

        public T Entity { get; set; }
        public S SearchCriteria { get; set; }
        public C CurrentDashboard { get; set; }
    }

    public class Routine2CustomAction<T, S, C>
    {

        public Routine2CustomAction(T entity, int step, S searchCriteria, C currentDashboard)
        {
            this.Entity = entity;
            this.Step = step;
            this.SearchCriteria = searchCriteria;
            this.CurrentDashboard = currentDashboard;
        }

        public T Entity { get; set; }
        public S SearchCriteria { get; set; }
        public C CurrentDashboard { get; set; }
        public int Step { get; set; }
    }

    public class Routine2LogButton
    {

        public Routine2LogButton(int routineId, int entityId)
        {
            this.EntityId = entityId;
            this.RoutineId = routineId;
        }

        public int EntityId { get; set; }
        public int RoutineId { get; set; }
    }
}
