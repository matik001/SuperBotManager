using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SuperBotManagerBackend.DB.Repositories
{
    [Table("refreshtoken")]
    public class RefreshToken : IEntity
    {
        [Key]
        [Column("RefreshTokenId")]
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GenerateNew(User user);
        Task<RefreshToken> GenerateNewAndRemoveOld(User user, string oldToken);
    }
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<RefreshToken> GenerateNew(User user)
        {
            var randomNumber = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var token = Convert.ToBase64String(randomNumber);
                var refreshToken = new RefreshToken
                {
                    Token = token,
                    CreationDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddDays(7),
                    UserId = user.Id
                };
                await Create(refreshToken);
                return refreshToken;
            }
        }

        public async Task<RefreshToken> GenerateNewAndRemoveOld(User user, string old)
        {
            if(old != null)
            {
                var oldToken = this.GetAll().Where(a => a.Token == old && a.UserId == user.Id).FirstOrDefault();
                if(oldToken != null)
                    await this.Delete(oldToken.Id);
            }

            return await GenerateNew(user);
        }
    }
}
