    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.EntityFrameworkCore;

    namespace EasyCode.Models
    {
        [Table("Attendances")]
        [Index(nameof(EnrollmentId), nameof(AttendanceDate), IsUnique = true)] // tránh điểm danh trùng ngày
        public class Attendance
        {
            [Key]
            public int AttendanceId { get; set; }

            [Required]
            [ForeignKey(nameof(Enrollment))]
            public int EnrollmentId { get; set; }

            [Required]
            public DateTime AttendanceDate { get; set; } = DateTime.Today; // ngày điểm danh

            [Required]
            public bool IsPresent { get; set; } = true; // thêm property IsPresent để khớp SQL BIT DEFAULT 1

            [NotMapped]
            public int WeekNumber 
            { 
                get
                {
                    var culture = System.Globalization.CultureInfo.CurrentCulture;
                    return culture.Calendar.GetWeekOfYear(
                        AttendanceDate, 
                        System.Globalization.CalendarWeekRule.FirstDay, 
                        DayOfWeek.Monday
                    );
                }
            }

            // Navigation property
            [Required]
            public virtual Enrollment Enrollment { get; set; } = null!;
        }
    }
