using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyCode.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyCode.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(512);
                entity.Property(e => e.PasswordResetTokenHash).HasMaxLength(256);
                entity.Property(e => e.AvatarPath).HasMaxLength(260);
            });
        }
    }
}