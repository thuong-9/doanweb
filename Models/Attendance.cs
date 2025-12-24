using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCode.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }

        [Required]
        public int EnrollmentId { get; set; }

        public DateTime AttendDate { get; set; } = DateTime.Today;

        public int WeekNumber { get; set; }

        public Enrollment? Enrollment { get; set; }
    }

}
