using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    /// <summary>
    ///  Field value is always string. Field type gives information about how to parse string to specific type and how to display it in the frontend.
    /// </summary>
    public enum FieldType
    {
        String,
        Number,
        Secret,
        DateTime, 
        Date, 
        Boolean,
        Json,
        Set,
        ExecutorPicker
    }
    public class SetOption
    {
        public string Display { get; set; }
        public string Value { get; set;}

        public SetOption(string display, string value)
        {
            Display = display;
            Value = value;
        }
    }
    public class FieldInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FieldType Type { get; set; }
        public bool IsOptional { get; set; } = false;
        public string? InitialValue { get; set; } = null;

        public List<SetOption>? SetOptions{ get; set; } = null;

        public FieldInfo(string name, FieldType type, string description = "", bool isOptional = false)
        {
            Name = name;
            Type = type;
            Description = description;
            IsOptional = isOptional;
        }
    }
    public class ActionDefinitionSchema
    {
        public List<FieldInfo> InputSchema { get; set; } = new List<FieldInfo>();
        public List<FieldInfo> OutputSchema { get; set; } = new List<FieldInfo>();
    }

    [Table("actiondefinition")]
    public class ActionDefinition : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public string ActionDefinitionName { get; set; }
        public string ActionDefinitionQueueName { get; set; } /// not present in the frontend (not in dto)
        public string ActionDefinitionDescription { get; set; }
        public string ActionDefinitionIcon { get; set; } /// can be relative or absolute url
        public ActionDefinitionSchema ActionDataSchema { get; set; }
        public bool PreserveExecutedInputs { get; set; }

        public virtual ICollection<ActionExecutor> ActionExecutors { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
    public interface IActionDefinitionRepository : IGenericRepository<ActionDefinition, int>
    {
    }
    public class ActionDefinitionRepository : GenericRepository<ActionDefinition, int>, IActionDefinitionRepository
    {
        public ActionDefinitionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
