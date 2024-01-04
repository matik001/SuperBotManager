using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SuperBotManagerBackend.DB.Repositories
{
    [Table("revokedtokens")]
    public class RevokedToken : IEntity
    {
        [Key]
        [Column("RevokedTokenId")]
        public int Id { get; set; }
        public string TokenGuid { get; set; }
        public DateTime ExpirationDate { get; set; } /// to know when to delete from db

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IRevokedTokenRepository : IGenericRepository<RevokedToken>
    {
        public bool IsTokenRevoked(string tokenGuid);
    }
    public class RevokedTokenRepository : GenericRepository<RevokedToken>, IRevokedTokenRepository
    {
        public RevokedTokenRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public bool IsTokenRevoked(string tokenGuid)
        {
            return GetAll().Any(t => t.TokenGuid == tokenGuid);
        }
    }
}
