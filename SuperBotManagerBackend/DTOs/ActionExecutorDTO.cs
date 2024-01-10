using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionExecutorCreateDTO
    {
        public string ActionExecutorName { get; set; }
        public ActionExecutorSchema ActionData { get; set; }

        public int ActionDefinitionId { get; set; }
        public RunMethod RunMethod { get; set; } = RunMethod.Manual;
        public bool PreserveExecutedInputs { get; set; }
        public int? ActionExecutorOnFinishId { get; set; }
    }
    public class ActionExecutorUpdateDTO : ActionExecutorCreateDTO
    {
        public int Id { get; set; }
    }
    public class ActionExecutorDTO : ActionExecutorUpdateDTO
    {
        public bool IsValid { get; set; } /// all inputs are filled correctly

        public DateTime? LastRunDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public class ActionExecutorExtendedDTO : ActionExecutorDTO
    {
        public ActionDefinitionDTO ActionDefinition { get; set; }
    }
}
