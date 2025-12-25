<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyCode.Models;

>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
namespace EasyCode.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }
<<<<<<< HEAD
=======

>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
<<<<<<< HEAD
=======
        [EmailAddress]
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(512)]
        public string Password { get; set; } = string.Empty;
<<<<<<< HEAD

    }
}
=======
        // ====== LIÊN KẾT ======
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
        [MaxLength(256)]
        public string? PasswordResetTokenHash { get; set; }
        public DateTime? PasswordResetTokenExpiresUtc { get; set; }
        [MaxLength(260)]
        public string? AvatarPath { get; set; }
    }
}
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
