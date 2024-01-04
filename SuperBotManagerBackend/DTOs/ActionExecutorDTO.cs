using SuperBotManagerBackend.DB.Repositories;
using SuperBotManagerBackend.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionExecutorCreateDTO
    {
        public string ActionExecutorName { get; set; }
        public ActionExecutorSchema ActionData { get; set; }

        public int ActionDefinitionId { get; set; }
        public RunPeriod RunPeriod { get; set; } = RunPeriod.Manual;
        public int? TimeIntervalSeconds { get; set; }
        public int? ActionExecutorOnFinishId { get; set; }
    }
    public class ActionExecutorDTO : ActionExecutorCreateDTO
    {
        public int Id { get; set; }
        
        public DateTime? LastRunDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
