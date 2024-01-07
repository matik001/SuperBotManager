using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperBotManagerBackend.DB.Repositories
{
    [Table("userpassword")]
    public class UserPassword : IEntity<int>
    {
        [Key]
        [Column("PasswordId")]
        public int Id { get; set; }
        public string PasswordHash { get; set; }
        public DateTime PasswordDate { get; set; }

        [ForeignKey("User")]
        public int PasswordUserId { get; set; }
        public virtual User User { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
    public interface IUserPasswordRepository : IGenericRepository<UserPassword, int>
    {
    }
    public class UserPasswordRepository : GenericRepository<UserPassword, int>, IUserPasswordRepository
    {
        public UserPasswordRepository(AppDBContext dbContext) : base(dbContext)
        {
        }
    }
}
