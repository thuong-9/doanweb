using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyCode.Models.Grading
{
    public static class CssGrader
    {
        public static GradingResult Grade(CssSubmission sub)
        {
            var result = new GradingResult();
            var checks = new List<(string key, Func<bool> pass, string desc)>();

            string html = sub.Html ?? string.Empty;
            string css = sub.Css ?? string.Empty;
            string htmlLower = html.ToLowerInvariant();
            string cssLower = css.ToLowerInvariant();

            // Xây rubric chi tiết theo SectionId và ExerciseIndex
            string exerciseKey = $"{sub.SectionId}-{sub.ExerciseIndex}";

            switch (exerciseKey)
            {
                // === CSS Cú pháp ===
                case "cssCuphap-1":
                    checks.Add(("has_p_tag", () => Regex.IsMatch(htmlLower, "<p[\\s>]"), "Có thẻ <p>"));
                    checks.Add(("has_p_selector", () => Regex.IsMatch(cssLower, "\\bp\\s*\\{"), "Có selector p trong CSS"));
                    checks.Add(("has_color", () => Regex.IsMatch(cssLower, "color\\s*:\\s*[^;]+;"), "Có thuộc tính color"));
                    checks.Add(("has_background_color", () => Regex.IsMatch(cssLower, "background-color\\s*:\\s*[^;]+;"), "Có thuộc tính background-color"));
                    break;

                case "cssCuphap-2":
                    checks.Add(("has_property", () => Regex.IsMatch(cssLower, "(color|font-size|font-family)\\s*:"), "Có property (color/font-size/font-family)"));
                    checks.Add(("has_value", () => Regex.IsMatch(cssLower, ":\\s*[a-z0-9#]+"), "Có value cho property"));
                    checks.Add(("proper_syntax", () => cssLower.Contains("{") && cssLower.Contains("}") && cssLower.Contains(":"), "Cú pháp đúng: selector { property: value; }"));
                    break;

                // === CSS Selector ===
                case "cssSelector-1":
                    checks.Add(("has_tag_selector", () => Regex.IsMatch(cssLower, "\\b(h1|h2|p)\\s*\\{"), "Có selector theo tên thẻ (h1/h2/p)"));
                    checks.Add(("has_class_html", () => Regex.IsMatch(htmlLower, "class\\s*="), "Có class trong HTML"));
                    checks.Add(("has_class_selector", () => Regex.IsMatch(cssLower, "\\.[a-z0-9_-]+\\s*\\{"), "Có selector theo class (.classname)"));
                    checks.Add(("has_id_html", () => Regex.IsMatch(htmlLower, "id\\s*="), "Có id trong HTML"));
                    checks.Add(("has_id_selector", () => Regex.IsMatch(cssLower, "#[a-z0-9_-]+\\s*\\{"), "Có selector theo id (#idname)"));
                    break;

                case "cssSelector-2":
                    checks.Add(("multi_selector", () => Regex.IsMatch(cssLower, "[a-z0-9]+\\s*,\\s*[a-z0-9]+\\s*\\{"), "Kết hợp nhiều selector (h1, h2)"));
                    checks.Add(("has_hover", () => cssLower.Contains(":hover"), "Có pseudo-class :hover"));
                    checks.Add(("has_first_child", () => cssLower.Contains(":first-child"), "Có pseudo-class :first-child"));
                    break;

                // === CSS Thực hiện ===
                case "cssThucHien-1":
                    checks.Add(("inline_style", () => Regex.IsMatch(htmlLower, "style\\s*=\\s*\""), "Có CSS inline trong thẻ HTML"));
                    checks.Add(("internal_style", () => Regex.IsMatch(htmlLower, "<style[\\s\\S]*?>[\\s\\S]*?</style>"), "Có CSS nội bộ trong <style>"));
                    checks.Add(("external_link", () => Regex.IsMatch(htmlLower, "<link[\\s\\S]*?rel\\s*=\\s*[\"']stylesheet[\"']"), "Có liên kết CSS ngoài <link rel=\"stylesheet\">"));
                    break;

                case "cssThucHien-2":
                    checks.Add(("inline_demo", () => Regex.IsMatch(htmlLower, "style\\s*="), "Có ví dụ CSS inline"));
                    checks.Add(("internal_demo", () => Regex.IsMatch(htmlLower, "<style"), "Có ví dụ CSS internal"));
                    checks.Add(("explain_diff", () => html.Length > 100, "Giải thích/demo đủ chi tiết (HTML dài > 100 ký tự)"));
                    break;

                // === CSS Comment ===
                case "cssComment-1":
                    checks.Add(("has_comment", () => cssLower.Contains("/*") && cssLower.Contains("*/"), "Có comment CSS /* ... */"));
                    checks.Add(("comment_explains", () => {
                        var match = Regex.Match(css, @"/\*(.+?)\*/", RegexOptions.Singleline);
                        return match.Success && match.Groups[1].Value.Trim().Length > 5;
                    }, "Comment có nội dung giải thích (>5 ký tự)"));
                    break;

                // === CSS Màu & Nền ===
                case "cssMauNen-1":
                    checks.Add(("bg_color", () => Regex.IsMatch(cssLower, "background-color\\s*:"), "Có background-color"));
                    checks.Add(("bg_image", () => Regex.IsMatch(cssLower, "background-image\\s*:\\s*url"), "Có background-image với url(...)"));
                    break;

                case "cssMauNen-2":
                    checks.Add(("bg_repeat", () => Regex.IsMatch(cssLower, "background-repeat\\s*:"), "Có background-repeat"));
                    checks.Add(("bg_position", () => Regex.IsMatch(cssLower, "background-position\\s*:"), "Có background-position"));
                    checks.Add(("bg_size", () => Regex.IsMatch(cssLower, "background-size\\s*:"), "Có background-size"));
                    break;

                case "cssMauNen-3":
                    checks.Add(("shorthand_bg", () => {
                        var match = Regex.Match(cssLower, "background\\s*:\\s*[^;{]+;");
                        return match.Success && match.Value.Split(' ').Length >= 2;
                    }, "Có background shorthand với nhiều giá trị"));
                    checks.Add(("uses_color", () => Regex.IsMatch(cssLower, "(color|background)\\s*:\\s*(#[0-9a-f]{3,6}|rgb|rgba|[a-z]+)"), "Sử dụng mã màu hoặc tên màu"));
                    break;

                // === CSS Border ===
                case "cssBorder-1":
                    checks.Add(("border_basic", () => Regex.IsMatch(cssLower, "\\bborder\\s*:"), "Có thuộc tính border cơ bản"));
                    checks.Add(("border_style", () => Regex.IsMatch(cssLower, "border(-style)?\\s*:\\s*(solid|dashed|dotted)"), "Có border-style (solid/dashed/dotted)"));
                    break;

                case "cssBorder-2":
                    checks.Add(("border_width", () => Regex.IsMatch(cssLower, "border(-width)?\\s*:\\s*\\d+px"), "Có border-width với giá trị px"));
                    checks.Add(("border_color", () => Regex.IsMatch(cssLower, "border-color\\s*:"), "Có border-color"));
                    checks.Add(("border_radius", () => Regex.IsMatch(cssLower, "border-radius\\s*:"), "Có border-radius để bo góc"));
                    break;

                case "cssBorder-3":
                    checks.Add(("border_specific", () => Regex.IsMatch(cssLower, "border-(top|bottom|left|right)\\s*:"), "Có border riêng cho cạnh (top/bottom/left/right)"));
                    checks.Add(("combined_props", () => {
                        int count = 0;
                        if (cssLower.Contains("border-width")) count++;
                        if (cssLower.Contains("border-style")) count++;
                        if (cssLower.Contains("border-color")) count++;
                        return count >= 2;
                    }, "Kết hợp ít nhất 2 trong 3: width, style, color"));
                    break;

                // === CSS Margin/Padding ===
                case "cssMarginPadding-1":
                    checks.Add(("has_margin", () => Regex.IsMatch(cssLower, "margin\\s*:"), "Có thuộc tính margin"));
                    checks.Add(("has_padding", () => Regex.IsMatch(cssLower, "padding\\s*:"), "Có thuộc tính padding"));
                    checks.Add(("use_px_or_percent", () => Regex.IsMatch(cssLower, "(margin|padding)\\s*:\\s*\\d+(px|%)"), "Sử dụng đơn vị px hoặc %"));
                    break;

                case "cssMarginPadding-2":
                    checks.Add(("margin_shorthand", () => {
                        var match = Regex.Match(cssLower, "margin\\s*:\\s*[\\d\\s]+px");
                        return match.Success && match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length >= 3;
                    }, "Có margin shorthand (2-4 giá trị)"));
                    checks.Add(("padding_shorthand", () => {
                        var match = Regex.Match(cssLower, "padding\\s*:\\s*[\\d\\s]+px");
                        return match.Success && match.Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length >= 3;
                    }, "Có padding shorthand (2-4 giá trị)"));
                    checks.Add(("center_technique", () => cssLower.Contains("margin") && cssLower.Contains("auto"), "Sử dụng margin: auto để căn giữa"));
                    break;

                // === CSS Menu ===
                case "cssMenu-1":
                    checks.Add(("has_ul_li", () => Regex.IsMatch(htmlLower, "<ul[\\s\\S]*?>[\\s\\S]*?<li"), "Có cấu trúc <ul><li> cho menu"));
                    checks.Add(("remove_list_style", () => Regex.IsMatch(cssLower, "list-style(-type)?\\s*:\\s*none"), "Xóa dấu đầu dòng list-style: none"));
                    checks.Add(("li_display", () => Regex.IsMatch(cssLower, "li\\s*\\{[^}]*display\\s*:\\s*(inline|inline-block|flex)"), "Đặt display cho li (inline/inline-block/flex)"));
                    break;

                case "cssMenu-2":
                    checks.Add(("flexbox_menu", () => cssLower.Contains("display") && cssLower.Contains("flex"), "Dùng Flexbox (display: flex)"));
                    checks.Add(("justify_content", () => Regex.IsMatch(cssLower, "justify-content\\s*:"), "Có justify-content để căn chỉnh"));
                    checks.Add(("hover_effect", () => cssLower.Contains(":hover"), "Có hiệu ứng :hover cho menu"));
                    break;

                case "cssMenu-3":
                    checks.Add(("vertical_menu", () => cssLower.Contains("flex-direction") || cssLower.Contains("display: block"), "Menu dọc (flex-direction hoặc display: block)"));
                    checks.Add(("styling_complete", () => {
                        int styleCount = 0;
                        if (cssLower.Contains("background")) styleCount++;
                        if (cssLower.Contains("padding")) styleCount++;
                        if (cssLower.Contains("border")) styleCount++;
                        if (cssLower.Contains("color")) styleCount++;
                        return styleCount >= 3;
                    }, "Có đầy đủ styling (background, padding, border, color - ít nhất 3)"));
                    checks.Add(("responsive_or_advanced", () => cssLower.Contains("@media") || cssLower.Contains("transition"), "Có tính năng nâng cao (@media hoặc transition)"));
                    break;

                default:
                    // Không biết bài cụ thể: chấm theo tiêu chí chung
                    checks.Add(("has_css", () => css.Trim().Length > 10, "Có CSS (>10 ký tự)"));
                    checks.Add(("has_html", () => html.Trim().Length > 10, "Có HTML (>10 ký tự)"));
                    checks.Add(("valid_syntax", () => cssLower.Contains("{") && cssLower.Contains("}"), "Cú pháp CSS cơ bản đúng"));
                    break;
            }

            int passed = 0;
            foreach (var (key, pass, desc) in checks)
            {
                try
                {
                    if (pass()) { result.PassedChecks.Add(desc); passed++; }
                    else { result.FailedChecks.Add(desc); }
                }
                catch
                {
                    result.FailedChecks.Add(desc + " (lỗi kiểm tra)");
                }
            }

            result.Score = checks.Count == 0 ? 0 : (int)Math.Round(100.0 * passed / checks.Count);
            result.Summary = $"Hoàn thành {passed}/{checks.Count} tiêu chí cho bài {sub.SectionId} - Bài {sub.ExerciseIndex}.";

            if (result.FailedChecks.Count > 0)
            {
                result.Suggestions = "Hãy bổ sung các tiêu chí còn thiếu: " + string.Join(", ", result.FailedChecks) + ".";
            }
            else
            {
                result.Suggestions = "Tuyệt vời! Bạn đã hoàn thành đầy đủ các yêu cầu.";
            }

            return result;
        }
    }
}
