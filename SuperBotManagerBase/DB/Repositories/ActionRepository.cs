using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    public class ActionSchema
    {
        public Dictionary<string, string> Input { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Output { get; set; } = new Dictionary<string, string>();
    }

    public enum ActionStatus
    {
        Pending, InProgress, Finished, Error
    }
    [Table("action")]
    public class Action : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public ActionSchema ActionData { get; set; }
        public ActionStatus ActionStatus { get; set; } = ActionStatus.Pending;


        [ForeignKey("ActionExecutor")]
        public int? ActionExecutorId { get; set; }
        public virtual ActionExecutor? ActionExecutor { get; set; }

        public int? ErrorId { get; set; }
        /// TODO Error object

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionRepository : IGenericRepository<Action, int>
    {

    }
    public class ActionRepository : GenericRepository<Action, int>, IActionRepository
    {
        public ActionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
