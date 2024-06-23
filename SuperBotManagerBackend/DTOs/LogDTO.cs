using SuperBotManagerBase.DB.Repositories;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperBotManagerBackend.DTOs
{
    public class LogCreateDTO
    {
        public string LogApp { get; set; }
        public string LogTitle { get; set; }
        public string? LogDetails { get; set; }
        public string LogModule { get; set; } /// string nie enum bo inne aplikacje moga z tego tez korzystac
        public LogType LogType { get; set; }

        public UserDTO User { get; set; }
        public int? UserId { get; set; }
    }
    public class LogDTO : LogCreateDTO
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
