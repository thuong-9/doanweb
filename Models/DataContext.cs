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
<<<<<<< HEAD
=======
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

<<<<<<< HEAD
            // Seed data for Exercises
            modelBuilder.Entity<Exercise>().HasData(
                new Exercise { Id = 1, Title = "Tạo tiêu đề HTML", Description = "Tạo một trang HTML đơn giản với tiêu đề.", Type = "HTML", Code = "<!DOCTYPE html>\n<html>\n<head>\n<title>My Page</title>\n</head>\n<body>\n<h1>Hello World</h1>\n</body>\n</html>", Solution = "<!DOCTYPE html>\n<html>\n<head>\n<title>My Page</title>\n</head>\n<body>\n<h1>Hello World</h1>\n</body>\n</html>", Hint = "Sử dụng thẻ <h1> để tạo tiêu đề." },
                new Exercise { Id = 2, Title = "Thêm đoạn văn", Description = "Thêm một đoạn văn vào trang HTML.", Type = "HTML", Code = "<!DOCTYPE html>\n<html>\n<body>\n<p>Đây là đoạn văn.</p>\n</body>\n</html>", Solution = "<!DOCTYPE html>\n<html>\n<body>\n<p>Đây là đoạn văn.</p>\n</body>\n</html>", Hint = "Sử dụng thẻ <p>." },
                new Exercise { Id = 3, Title = "Style CSS cơ bản", Description = "Thêm CSS để làm cho tiêu đề màu đỏ.", Type = "CSS", Code = "<!DOCTYPE html>\n<html>\n<head>\n<style>\nh1 { color: red; }\n</style>\n</head>\n<body>\n<h1>Hello</h1>\n</body>\n</html>", Solution = "<!DOCTYPE html>\n<html>\n<head>\n<style>\nh1 { color: red; }\n</style>\n</head>\n<body>\n<h1>Hello</h1>\n</body>\n</html>", Hint = "Sử dụng thuộc tính color trong CSS." }
            );
=======
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.UserName).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
                entity.Property(e => e.Password).IsRequired().HasMaxLength(512);
                entity.Property(e => e.PasswordResetTokenHash).HasMaxLength(256);
                entity.Property(e => e.AvatarPath).HasMaxLength(260);
            });
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
        }
    }
}