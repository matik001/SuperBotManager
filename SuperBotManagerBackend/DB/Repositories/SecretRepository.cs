using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SuperBotManagerBackend.Configuration;
using SuperBotManagerBackend.DTOs;
using SuperBotManagerBackend.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBackend.DB.Repositories
{


    [Table("secret")]
    public class Secret : IEntity<Guid>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public byte[] SecretIV { get; set; }
        public byte[] SecretValue { get; set; }

        private string _decryptedValue;

        [NotMapped]
        public string DecryptedSecretValue
        {
            get
            {
                if(_decryptedValue == null)
                {
                    _decryptedValue = EncyptionUtils.DecryptAES(this.SecretValue, EncryptionConfig.Key, this.SecretIV);
                }
                return _decryptedValue;
            }
            set
            {
                _decryptedValue = value;
                this.SecretIV = EncyptionUtils.GenerateIV();
                this.SecretValue = EncyptionUtils.EncryptAES(_decryptedValue, EncryptionConfig.Key, this.SecretIV);
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface ISecretRepository : IGenericRepository<Secret, Guid>
    {

    }
    public class SecretRepository : GenericRepository<Secret, Guid>, ISecretRepository
    {
        public SecretRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
