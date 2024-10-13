using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using App.Data;
using App.Data.Entities.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities.Models
{
    public class UserEntity : EntityBase, IHasEnabled
    {

        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? ResetPasswordToken { get; set; }
        public int RoleId { get; set; }
        public bool Enabled { get; set; } = true;

        //Navigation
        public RoleEntity Role { get; set; } = null!;

    }

    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.UserName).IsRequired().HasMaxLength(50);
            builder.HasIndex(e => e.UserName).IsUnique();
            builder.Property(e => e.Email).IsRequired().HasMaxLength(100);
            builder.HasIndex(e => e.Email).IsUnique();
            builder.Property(e => e.PasswordHash).IsRequired().HasMaxLength(255);
            builder.Property(e => e.RoleId).IsRequired();
            builder.Property(e => e.Enabled).IsRequired().HasDefaultValue(true);
            builder.Property(e => e.CreatedAt).IsRequired();

            builder.HasOne(d => d.Role)
                .WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            new UserEntitySeed().SeedData(builder);
        }
    }

    internal class UserEntitySeed : IEntityTypeSeed<UserEntity>
    {
        public void SeedData(EntityTypeBuilder<UserEntity> builder)
        {
            string password = BCrypt.Net.BCrypt.HashPassword("1234");
            builder.HasData(
                new UserEntity() { Id = 1, UserName = "admin", Email = "a@a.com", Enabled = true, RoleId = 1, PasswordHash = password, CreatedAt = DateTime.UtcNow },
                new UserEntity() { Id = 2, UserName = "firstCommenter", Email = "u@u.com", Enabled = true, RoleId = 2, PasswordHash = password, CreatedAt = DateTime.UtcNow }

            );
        }
    }
}
