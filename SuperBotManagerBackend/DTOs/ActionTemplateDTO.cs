using SuperBotManagerBackend.DB.Repositories;
using SuperBotManagerBackend.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionTemplateCreateDTO
    {
        public string ActionTemplateName { get; set; }
        public ActionTemplateSchema ActionData { get; set; }

        public int ActionDefinitionId { get; set; }
        public RunPeriod RunPeriod { get; set; } = RunPeriod.Single;
        public int? TimeIntervalSeconds { get; set; }
        public int? ActionTemplateOnFinishId { get; set; }
    }
    public class ActionTemplateDTO : ActionTemplateCreateDTO
    {
        public int Id { get; set; }
        
        public DateTime? LastRunDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
