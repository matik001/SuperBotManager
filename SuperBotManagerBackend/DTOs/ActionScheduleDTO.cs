using SuperBotManagerBase.DB.Repositories;

namespace SuperBotManagerBackend.DTOs
{
    public class ActionScheduleCreateDTO
    {
        public string ActionSCheduleName { get; set; }
        public int ExecutorId { get; set; }
        public DateTime NextRun { get; set; }
        public bool Enabled { get; set; }
        public ActionScheduleType Type { get; set; }
        public int IntervalSec { get; set; }
    }
    public class ActionScheduleUpdateDTO : ActionScheduleCreateDTO
    {
        public int Id { get; set; }
    }
    public class ActionScheduleDTO : ActionScheduleUpdateDTO
    {
        public ActionExecutor Executor { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
