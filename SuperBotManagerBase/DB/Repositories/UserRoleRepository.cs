using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    public class UserRole : IEntity<int>
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public interface IUserRoleRepository: IGenericRepository<UserRole, int>
    {
    }

    public class UserRoleRepository : GenericRepository<UserRole, int>, IUserRoleRepository
    {
        public UserRoleRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
