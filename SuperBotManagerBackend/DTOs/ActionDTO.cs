using SuperBotManagerBackend.DB.Repositories;
using SuperBotManagerBackend.DB;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionCreateDTO
    {
        public ActionSchema ActionData { get; set; }
        public int ActionExecutorId { get; set; }
    }
    public class ActionDTO
    {
        public int Id { get; set; }

        public ActionSchema ActionData { get; set; }
        public ActionStatus ActionStatus { get; set; } = ActionStatus.Pending;

        public int? ActionExecutorId { get; set; }

        public int? ErrorId { get; set; }
        /// TODO Error object

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
