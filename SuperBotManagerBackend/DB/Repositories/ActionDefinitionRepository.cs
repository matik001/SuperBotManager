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
    /// <summary>
    ///  Field value is always string. Field type gives information about how to parse string to specific type and how to display it in the frontend.
    /// </summary>
    public enum FieldType
    {
        String,
        Number,
        DateTime, /// yyyy-MM-dd HH:mm:ss
        Date, /// yyyy-MM-dd
        Boolean,
        Json,
        Set
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
        public List<SetOption>? SetOptions{ get; set; } = null;

        public FieldInfo(string name, FieldType type, string description = "")
        {
            Name = name;
            Type = type;
            Description = description;
        }
    }
    public class ActionDefinitionSchema
    {
        public List<FieldInfo> InputSchema { get; set; } = new List<FieldInfo>();
        public List<FieldInfo> OutputSchema { get; set; } = new List<FieldInfo>();
    }

    [Table("actiondefinition")]
    public class ActionDefinition : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionDefinitionName { get; set; }
        public string ActionDefinitionDescription { get; set; }
        public string ActionDefinitionIcon { get; set; } /// can be relative or absolute url
        public ActionDefinitionSchema ActionDataSchema { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionDefinitionRepository : IGenericRepository<ActionDefinition>
    {

    }
    public class ActionDefinitionRepository : GenericRepository<ActionDefinition>, IActionDefinitionRepository
    {
        public ActionDefinitionRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
