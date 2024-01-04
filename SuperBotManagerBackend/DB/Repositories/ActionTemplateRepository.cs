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
    public enum FieldType
    {
        String,
        Int,
        DateTime
    }
    public class FieldInfo
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
    }
    public class ActionTemplateSchema
    {
        public Dictionary<string, FieldInfo> InputSchema { get; set; } = new Dictionary<string, FieldInfo>();
        public Dictionary<string, FieldInfo> OutputSchema { get; set; } = new Dictionary<string, FieldInfo>();
    }

    [Table("actiontemplate")]
    public class ActionTemplate : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string ActionTemplateName { get; set; }

        public ActionTemplateSchema ActionDataSchema { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IActionTemplateRepository : IGenericRepository<ActionTemplate>
    {

    }
    public class ActionTemplateRepository : GenericRepository<ActionTemplate>, IActionTemplateRepository
    {
        public ActionTemplateRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
