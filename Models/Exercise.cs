using System.ComponentModel.DataAnnotations;

namespace EasyCode.Models
{
    public class Exercise
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public string? Type { get; set; } // HTML, CSS, etc.

        public string? Code { get; set; } // Sample code

        public string? Solution { get; set; } // Expected solution

        public string? Hint { get; set; }
    }
}