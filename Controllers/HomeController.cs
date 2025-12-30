using Microsoft.AspNetCore.Mvc;
using EasyCode.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EasyCode.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult DangKy()
        {
            string? email = null;
            if (User.Identity?.IsAuthenticated == true)
            {
                email = User.FindFirstValue(ClaimTypes.Email);
            }

            ViewBag.UserEmail = email ?? "";
            return View();
        }
        [HttpGet]
public IActionResult CheckEmail(string email)
{
    bool exists = _context.Enrollments.Any(e => e.Email == email);
    return Json(new { exists });
}

        // POST: gửi OTP
        [HttpPost]
        public IActionResult GuiOTP(string sdt)
        {
            var otp = new Random().Next(100000, 999999).ToString();
            HttpContext.Session.SetString("OTP", otp);
            Console.WriteLine($"OTP gửi tới {sdt}: {otp}");

            return Json(new { success = true });
        }

        // POST: xác nhận OTP
        [HttpPost]
        public IActionResult XacNhanOTP(string otp)
        {
            var realOtp = HttpContext.Session.GetString("OTP");
            return Json(new { success = otp == realOtp });
        }

        // POST: lưu đăng ký vào DB
        [HttpPost]
        [Authorize]
        public IActionResult DangKyThanhCong()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var username = User.Identity?.Name ?? "Unknown";

            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập!" });
            }

            // check nếu email đã đăng ký
            if (_context.Enrollments.Any(e => e.Email == email))
            {
                return Json(new { success = false, message = "Email này đã đăng ký khóa học!" });
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return Json(new { success = false, message = "Không tìm thấy tài khoản!" });
            }

            var enrollment = new Enrollment
            {
                UserID = user.UserID,
                UserName = username,
                Email = email
            };

            _context.Enrollments.Add(enrollment);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("HasCourse", 1);
            HttpContext.Session.SetInt32("EnrollmentId", enrollment.EnrollmentId);

            return Json(new { success = true, redirect = Url.Action("Index") });
        }
    }
}
