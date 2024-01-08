using SuperBotManagerBase.DB.Repositories;
using SuperBotManagerBase.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionDefinitionDTO
    {
        public int Id { get; set; }
        public string ActionDefinitionName { get; set; }
        public string ActionDefinitionDescription { get; set; }
        public string ActionDefinitionIcon { get; set; }
        public bool PreserveExecutedInputs { get; set; }
        public ActionDefinitionSchema ActionDataSchema { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
