using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SuperBotManagerBase.DB.Repositories
{
    [Table("user")]
    public class User : IEntity<int>
    {
        [Key]
        [Column("UserId")]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }


        public virtual ICollection<UserPassword> UserPasswords { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = [];
        public virtual ICollection<Role> Roles { get; set; } = [];

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<RevokedToken> RevokedTokens { get; set; }

        public virtual ICollection<VaultItem> VaultItems { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<User> GetByUserName(string userName, Func<IQueryable<User>, IQueryable<User>> queryFn = null);

        UserPassword GetCurrentUserPassword(User user);
    }
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetByUserName(string userName, Func<IQueryable<User>, IQueryable<User>> queryFn = null)
        {
            if(queryFn == null)
                queryFn = a=>a;
            var userSet = _dbContext.Set<User>();
            var query = queryFn(userSet.AsQueryable());
            
            return await query.AsNoTracking()
                    .FirstOrDefaultAsync(e => e.UserName == userName);
        }



        public UserPassword GetCurrentUserPassword(User user)
        {
            var password = _dbContext.Entry(user)
                .Collection(b => b.UserPasswords)
                .Query()
                .OrderByDescending(a => a.PasswordDate)
                .FirstOrDefault();
            return password;
        }
    }
}
