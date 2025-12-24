using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EasyCode.Controllers
{
    public class LessonController : Controller
    {
        private readonly ILogger<LessonController> _logger;

        public LessonController(ILogger<LessonController> logger)
        {
            _logger = logger;
        }

        private bool CanShowAttendance()
        {
            if (!User.Identity.IsAuthenticated)
                return false;

            return HttpContext.Session.GetInt32("HasCourse") == 1;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Indexhtml));
        }

        public IActionResult Indexhtml()
        {
            ViewBag.ShowAttendance = CanShowAttendance();
            return View();
        }

        public IActionResult Indexcss()
        {
            ViewBag.ShowAttendance = CanShowAttendance();
            return View();
        }
    }
}
