using App.Data.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data.Entities
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
