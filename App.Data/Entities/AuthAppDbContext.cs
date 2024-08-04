using Auth.Api.Data.Models;

using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Data
{
    public class AuthAppDbContext : DbContext
    {
        public AuthAppDbContext(DbContextOptions<AuthAppDbContext> options) : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
           
        }
    }

    
}
