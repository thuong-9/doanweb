using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string Password { get; set; } = string.Empty;

        [MaxLength(256)]
        public string? PasswordResetTokenHash { get; set; }

        public DateTime? PasswordResetTokenExpiresUtc { get; set; }

        [MaxLength(260)]
        public string? AvatarPath { get; set; }

    }
}