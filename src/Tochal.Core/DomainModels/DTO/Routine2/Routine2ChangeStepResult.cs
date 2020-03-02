
using Tochal.Core.DomainModels.SSOT;

namespace Tochal.Core.DomainModels.DTO.Routine2
{
    public class Routine2ChangeStepResult
    {
        public int EntityId { get; set; }
        public int FromStep { get; set; }
        public int ToStep { get; set; }
        public int UserId { get; set; }
        public int RoutineId { get; set; }
        public string Description { get; set; }
        public Routine2Actions Action { get; set; }
    }
}