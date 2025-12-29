namespace EasyCode.Models.Grading
{
    public class CssSubmission
    {
        public string SectionId { get; set; } = string.Empty;    // ví dụ: cssCuphap, cssSelector
        public int ExerciseIndex { get; set; } = 1;              // số bài: 1, 2, 3...
        public string Html { get; set; } = string.Empty;         // mã HTML học viên nộp
        public string Css { get; set; } = string.Empty;          // mã CSS học viên nộp
    }
}
