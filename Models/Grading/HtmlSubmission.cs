namespace EasyCode.Models.Grading
{
    public class HtmlSubmission
    {
        public string SectionId { get; set; } = string.Empty; // ví dụ: cautruc, lienKet
        public int ExerciseIndex { get; set; } = 1;           // số bài: 1,2,3
        public string Html { get; set; } = string.Empty;      // mã HTML học viên nộp
    }
}
