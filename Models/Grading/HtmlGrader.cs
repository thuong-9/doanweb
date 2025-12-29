using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EasyCode.Models.Grading
{
    public static class HtmlGrader
    {
        public static GradingResult Grade(HtmlSubmission sub)
        {
            var result = new GradingResult();
            var checks = new List<(string key, Func<bool> pass, string desc)>();

            string html = sub.Html ?? string.Empty;
            string lower = html.ToLowerInvariant();

            string sectionKey = (sub.SectionId ?? string.Empty).ToLowerInvariant();
            string key = $"{sectionKey}-{sub.ExerciseIndex}";

            switch (key)
            {
                // Cấu trúc
                case "cautruc-1":
                    checks.Add(("doctype", () => lower.Contains("<!doctype html"), "Có khai báo <!DOCTYPE html>"));
                    checks.Add(("lang_vi", () => lower.Contains("<html") && lower.Contains("lang=\"vi\""), "Thẻ <html> có lang=\"vi\""));
                    checks.Add(("meta_utf8", () => lower.Contains("charset=\"utf-8\""), "Meta charset UTF-8"));
                    checks.Add(("has_title", () => Regex.IsMatch(lower, "<title>[\\s\\S]*?</title>"), "Có <title>"));
                    checks.Add(("body_h1_p", () => lower.Contains("<h1") && lower.Contains("<p"), "Trong body có h1 và p"));
                    break;
                case "cautruc-2":
                    checks.Add(("head_elements", () => lower.Contains("<head") && lower.Contains("<title") && lower.Contains("charset="), "Phần head có title + meta charset"));
                    checks.Add(("body_h1_p", () => lower.Contains("<body") && lower.Contains("<h1") && lower.Contains("<p"), "Body có h1 và p mô tả"));
                    break;
                case "cautruc-3":
                    checks.Add(("copyright", () => lower.Contains("&copy;") || lower.Contains("©"), "Có ký tự ©"));
                    checks.Add(("trademark", () => lower.Contains("&trade;") || lower.Contains("™"), "Có ký tự ™"));
                    checks.Add(("lt_gt", () => lower.Contains("&lt;") && lower.Contains("&gt;"), "Có &lt; và &gt;"));
                    checks.Add(("heart", () => lower.Contains("&hearts;") || lower.Contains("♥"), "Có ký tự ♥"));
                    break;

                // Soạn thảo
                case "soanthao-1":
                    checks.Add(("headings", () => lower.Contains("<h1") || lower.Contains("<h2"), "Có tiêu đề h1/h2"));
                    checks.Add(("paragraph", () => lower.Contains("<p"), "Có đoạn văn p"));
                    checks.Add(("br_tag", () => lower.Contains("<br"), "Có thẻ br xuống dòng"));
                    break;
                case "soanthao-2":
                    checks.Add(("br", () => lower.Contains("<br"), "Có thẻ br"));
                    checks.Add(("hr", () => lower.Contains("<hr"), "Có thẻ hr"));
                    checks.Add(("formatting", () => lower.Contains("<b") || lower.Contains("<strong") || lower.Contains("<i") || lower.Contains("<em") || lower.Contains("<u"), "Có thẻ định dạng chữ (b/i/u)"));
                    break;

                // Thẻ cơ bản
                case "thecoban-1":
                    checks.Add(("color", () => lower.Contains("color:"), "Có thuộc tính màu (style color)"));
                    checks.Add(("fontsize", () => Regex.IsMatch(lower, "font-size\\s*:\\s*\\d"), "Có thuộc tính font-size"));
                    break;
                case "thecoban-2":
                    checks.Add(("title_attr", () => lower.Contains("title="), "Có thuộc tính title"));
                    checks.Add(("id_attr", () => lower.Contains("id="), "Có thuộc tính id"));
                    checks.Add(("class_attr", () => lower.Contains("class="), "Có thuộc tính class"));
                    break;

                // Liên kết
                case "lienket-1":
                    checks.Add(("links", () => Regex.IsMatch(lower, "<a[\\s\\S]*?href=\"https?://"), "Có liên kết ngoài với href"));
                    checks.Add(("target_blank", () => lower.Contains("target=\"_blank\""), "Dùng target=\"_blank\""));
                    break;
                case "lienket-2":
                    checks.Add(("internal_links", () => Regex.IsMatch(lower, "href=\"#[a-z0-9_-]+\""), "Liên kết nội bộ href=#id"));
                    checks.Add(("ids", () => Regex.IsMatch(lower, "id=\"[a-z0-9_-]+\""), "Có id cho điểm neo"));
                    break;
                case "lienket-3":
                    checks.Add(("link_target", () => lower.Contains("target=\"_blank\""), "Liên kết mở tab mới"));
                    checks.Add(("image_link", () => Regex.IsMatch(lower, "<a[\\s\\S]*?><img[\\s\\S]*?></a>"), "Ảnh gắn trong liên kết"));
                    break;

                // Hình ảnh
                case "hinhanh-1":
                    checks.Add(("img_src", () => Regex.IsMatch(lower, "<img[\\s\\S]*?src=\""), "Có ảnh với src"));
                    checks.Add(("alt_attr", () => Regex.IsMatch(lower, "<img[\\s\\S]*?alt=\""), "Ảnh có alt"));
                    break;
                case "hinhanh-2":
                    checks.Add(("img_size", () => lower.Contains("width") || lower.Contains("height"), "Thiết lập width/height"));
                    checks.Add(("center_img", () => lower.Contains("center") || lower.Contains("text-align"), "Căn giữa hoặc dùng <center>"));
                    break;

                // Bảng biểu
                case "bangbieu-1":
                    checks.Add(("table", () => lower.Contains("<table"), "Có bảng <table>"));
                    checks.Add(("rows_cells", () => lower.Contains("<tr") && lower.Contains("<td"), "Có hàng (tr) và ô (td)"));
                    break;
                case "bangbieu-2":
                    checks.Add(("thead_tbody", () => lower.Contains("<thead") && lower.Contains("<tbody"), "Có thead và tbody"));
                    checks.Add(("th", () => lower.Contains("<th"), "Có tiêu đề cột th"));
                    break;
                case "bangbieu-3":
                    checks.Add(("merge", () => lower.Contains("rowspan") || lower.Contains("colspan"), "Có gộp ô (rowspan/colspan)"));
                    checks.Add(("caption", () => lower.Contains("<caption"), "Có tiêu đề bảng caption"));
                    break;

                // Danh sách
                case "danhsach-1":
                    checks.Add(("ul_li", () => lower.Contains("<ul") && lower.Contains("<li"), "Danh sách không thứ tự (ul/li)"));
                    break;
                case "danhsach-2":
                    checks.Add(("ol_li", () => lower.Contains("<ol") && lower.Contains("<li"), "Danh sách có thứ tự (ol/li)"));
                    break;
                case "danhsach-3":
                    checks.Add(("nested", () => Regex.IsMatch(lower, "<ul[\\s\\S]*?<ul") || Regex.IsMatch(lower, "<ol[\\s\\S]*?<ol"), "Danh sách lồng nhau"));
                    break;

                // Chú thích / trích dẫn
                case "chuthich-1":
                    checks.Add(("comment", () => lower.Contains("<!--"), "Có chú thích HTML <!-- -->"));
                    checks.Add(("cite", () => lower.Contains("<cite") || lower.Contains("<blockquote"), "Có thẻ trích dẫn (cite/blockquote)"));
                    break;
                case "chuthich-2":
                    checks.Add(("blockquote", () => lower.Contains("<blockquote"), "Có blockquote"));
                    checks.Add(("q_tag", () => lower.Contains("<q"), "Có thẻ q trích dẫn ngắn"));
                    break;

                default:
                    checks.Add(("has_html", () => lower.Contains("<html") || lower.Contains("<body"), "Có mã HTML"));
                    break;
            }

            int passed = 0;
            foreach (var (keyName, pass, desc) in checks)
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
            result.Suggestions = result.FailedChecks.Count > 0
                ? "Bổ sung các tiêu chí: " + string.Join(", ", result.FailedChecks) + "."
                : "Tốt lắm! Đã đủ yêu cầu.";

            return result;
        }
    }
}
