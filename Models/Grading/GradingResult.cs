using System.Collections.Generic;

namespace EasyCode.Models.Grading
{
    public class GradingResult
    {
        public int Score { get; set; }                 // 0 - 100
        public string Summary { get; set; } = string.Empty; // Tóm tắt kết quả
        public List<string> PassedChecks { get; set; } = new();
        public List<string> FailedChecks { get; set; } = new();
        public string? Suggestions { get; set; }        // Gợi ý cải thiện
    }
}
