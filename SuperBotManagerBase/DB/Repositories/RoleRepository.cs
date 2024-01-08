using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    [Table("role")]
    public class Role : IEntity<int>
    {
        [Key]
        [Column("RoleId")]
        public int Id { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<User> Users { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
    public enum RolesEnum
    {
        Admin = 1,
        Blocked = 2
    }

    public interface IRoleRepository : IGenericRepository<Role, int>
    {
        Task<Role> GetOrCreate(string roleName);
    }

    public class RoleRepository : GenericRepository<Role, int>, IRoleRepository
    {
        public RoleRepository(AppDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role>GetOrCreate(string roleName)
        {
            var role = await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.RoleName == roleName);
            if (role == null)
            {
                var newRole = new Role { RoleName = roleName };
                await this.Create(newRole);
                return newRole; 
            }
            return role;
        }


        public static void Seed(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(
                new Role() { Id = (int)RolesEnum.Admin, RoleName = "Admin" },
                new Role() { Id = (int)RolesEnum.Blocked, RoleName = "Blocked" }
            );
        }
    }
}
