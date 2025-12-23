using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Indexhtml));
        }

        public IActionResult Indexhtml()
        {
            return View();
        }

        public IActionResult Indexcss()
        {
            return View();
        }

    }
}