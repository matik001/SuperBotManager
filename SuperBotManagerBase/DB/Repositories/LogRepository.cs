using SuperBotManagerBase.Configuration;
using SuperBotManagerBase.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.DB.Repositories
{
    public enum LogType
    {
        Info,
        Warning,
        Error,
        Debug
    }
    [Table("log")]
    public class Log : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string LogApp{ get; set; }
        public string LogTitle { get; set; }
        public string? LogDetails{ get; set; }
        public string LogModule { get; set; } /// string nie enum bo inne aplikacje moga z tego tez korzystac
        public LogType LogType { get; set; }

        public User User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }



        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface ILogRepository : IGenericRepository<Log, int>
    {

    }
    public class LogRepository : GenericRepository<Log, int>, ILogRepository
    {
        public LogRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
