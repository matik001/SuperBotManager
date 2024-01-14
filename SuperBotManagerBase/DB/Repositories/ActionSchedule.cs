using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{ 
    public enum ActionScheduleType
    {
        Period, Once
    }
    [Table("actionschedule")]
    public class ActionSchedule: IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string ActionSCheduleName { get; set; }
        public int ExecutorId { get; set; }
        public virtual ActionExecutor Executor { get; set; }

        public DateTime NextRun { get; set; }
        public bool Enabled { get; set; }
        public ActionScheduleType Type { get; set; }
        public int IntervalSec { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
    public interface IActionScheduleRepository : IGenericRepository<ActionSchedule, int>
    {
    }
    public class ActionScheduleRepository : GenericRepository<ActionSchedule, int>, IActionScheduleRepository
    {
        public ActionScheduleRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
