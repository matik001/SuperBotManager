﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace SuperBotManagerBase.DB.Repositories
{
    [Table("userrole")]
    public class UserRole : IEntity<int>
    {
        [Key]
        [Column("UserRoleId")]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }


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
