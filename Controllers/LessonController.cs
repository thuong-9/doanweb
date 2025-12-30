using EasyCode.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace EasyCode.Controllers
{
    [Authorize]
    public class LessonController : Controller
    {
        private readonly DataContext _context;

        public LessonController(DataContext context)
        {
            _context = context;
        }

        private bool CanShowAttendance()
        {
            return User.Identity!.IsAuthenticated &&
                   HttpContext.Session.GetInt32("HasCourse") == 1;
        }

        private int GetEnrollmentId()
        {
            return HttpContext.Session.GetInt32("EnrollmentId") ?? 0;
        }

        private void LoadAttendanceViewBag()
        {
            ViewBag.ShowAttendance = CanShowAttendance();

            if (!CanShowAttendance())
            {
                ViewBag.CheckedToday = false;
                ViewBag.LastCheckedDay = 0;
                return;
            }

            int enrollmentId = GetEnrollmentId();
            var today = DateTime.Today;

            var records = _context.Attendances
                .Where(a => a.EnrollmentId == enrollmentId)
                .OrderBy(a => a.AttendanceDate)
                .ToList();

            ViewBag.CheckedToday = records.Any(a => a.AttendanceDate.Date == today);

            int totalDays = records.Count;
            ViewBag.LastCheckedDay = totalDays == 0
                ? 0
                : ((totalDays - 1) % 7) + 1;
        }
        public IActionResult Indexhtml()
        {
            LoadAttendanceViewBag();
            return View();
        }
        public IActionResult Indexcss()
        {
            LoadAttendanceViewBag();
            return View();
        }
}}
            