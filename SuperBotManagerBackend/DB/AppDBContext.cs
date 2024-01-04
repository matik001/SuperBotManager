using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SuperBotManagerBackend.DB.Repositories;
using System.Reflection.Emit;
using Action = SuperBotManagerBackend.DB.Repositories.Action;

namespace SuperBotManagerBackend.DB
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserPassword> UserPasswords { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<ActionTemplate> ActionTemplates { get; set; }
        public DbSet<ActionDefinition> ActionDefinitions { get; set; }
        public DbSet<Action> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ActionTemplate>()
                .Property(a => a.ActionDataSchema)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ActionTemplateSchema>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            builder.Entity<ActionDefinition>()
                .Property(a => a.ActionData)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ActionDefinitionSchema>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));


            builder.Entity<Action>()
                .Property(a => a.ActionData)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ActionSchema>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
         

            //builder.Entity<User>()
            //        .Property(a => a.CreationDate)
            //        .HasConversion(v => v,
            //            v => new DateTime(v.Ticks, DateTimeKind.Utc));

            //builder.Entity<RefreshToken>()
            //        .Property(a => a.CreationDate)
            //        .HasConversion(v => v,
            //            v => new DateTime(v.Ticks, DateTimeKind.Utc));

            //builder.Entity<RefreshToken>()
            //        .Property(a => a.ExpirationDate)
            //        .HasConversion(v => v,
            //            v => new DateTime(v.Ticks, DateTimeKind.Utc));

            //builder.Entity<RevokedToken>()
            //        .Property(a => a.ExpirationDate)
            //        .HasConversion(v => v,
            //            v => new DateTime(v.Ticks, DateTimeKind.Utc));

            //builder.Entity<Room>()
            //        .Property(a => a.CreationDate)
            //        .HasConversion(v => v,
            //            v => new DateTime(v.Ticks, DateTimeKind.Utc));

            /////
            ///// relations
            /////

            builder.Entity<User>()
                .HasMany(e => e.UserPasswords)
                .WithOne(e => e.User);

            builder.Entity<User>()
                .HasMany(e => e.RefreshTokens)
                .WithOne(e => e.User);

            builder.Entity<User>()
                .HasMany(e => e.RevokedTokens)
                .WithOne(e => e.User);


            /////
            ///// Seeds
            /////
            ///
            RoleRepository.Seed(builder.Entity<Role>());
        }

        private void _setModificationDate()
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is IEntity && (
                      e.State == EntityState.Added
                      || e.State == EntityState.Modified));

            foreach(var entityEntry in entries)
            {
                ((IEntity)entityEntry.Entity).ModifiedDate = DateTime.UtcNow;

                if(entityEntry.State == EntityState.Added)
                {
                    ((IEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
                }
            }
        }
        public override int SaveChanges()
        {
            _setModificationDate();
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _setModificationDate();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
