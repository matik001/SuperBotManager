using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SuperBotManagerBackend.DTOs;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBackend.DB.Repositories
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
    public class Action : IEntity
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
    public interface IActionRepository : IGenericRepository<Action>
    {

    }
    public class ActionRepository : GenericRepository<Action>, IActionRepository
    {
        public ActionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
