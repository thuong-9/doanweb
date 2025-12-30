using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyCode.Models;

namespace EasyCode.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [MaxLength(256)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string Password { get; set; } = string.Empty;
        // ====== LIÊN KẾT ======
        public ICollection<Enrollment>? Enrollments { get; set; }
        [MaxLength(256)]
        public string? PasswordResetTokenHash { get; set; }
        public DateTime? PasswordResetTokenExpiresUtc { get; set; }
        [MaxLength(260)]
        public string? AvatarPath { get; set; }
    }
}
