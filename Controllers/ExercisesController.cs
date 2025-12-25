<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
using EasyCode.Models;
=======
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f

namespace EasyCode.Controllers
{
    
    public class ExercisesController : Controller
    {
        private readonly ILogger<ExercisesController> _logger;
<<<<<<< HEAD
        private static readonly List<Exercise> _exercises = new List<Exercise>
        {
            new Exercise { Id = 1, Title = "Tạo trang HTML đầu tiên", Description = "Tạo một trang HTML đơn giản với cấu trúc cơ bản", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Trang HTML đầu tiên của tôi</title>\n</head>\n<body>\n  <h1>Xin chào thế giới!</h1>\n  <p>Đây là trang HTML đầu tiên tôi tạo bằng tay.</p>\n</body>\n</html>", Hint = "Theo yêu cầu" },
            new Exercise { Id = 2, Title = "Phân biệt các phần <head> và <body>", Description = "Tạo trang HTML với phần <head> và <body>", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Phân biệt phần head và body</title>\n</head>\n<body>\n  <h1>Đây là phần thân (body) của trang</h1>\n  <p>Phần <head> chứa thông tin về trang, còn <body> là nơi hiển thị nội dung.</p>\n</body>\n</html>", Hint = "Theo yêu cầu" },
            new Exercise { Id = 3, Title = "Thêm ký tự đặc biệt", Description = "Tạo trang HTML hiển thị các ký tự đặc biệt", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Ký tự đặc biệt trong HTML</title>\n</head>\n<body>\n  <h1>Các ký tự đặc biệt trong HTML</h1>\n  <p>Bản quyền: &copy; 2025 EasyCode</p>\n  <p>Thương hiệu: EasyCode&trade;</p>\n  <p>Ký hiệu nhỏ hơn: &lt; và lớn hơn: &gt;</p>\n  <p>Trái tim: &hearts;</p>\n</body>\n</html>", Hint = "Sử dụng entity codes" }
        };
=======
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f

        public ExercisesController(ILogger<ExercisesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

<<<<<<< HEAD
        public IActionResult Html()
        {
            var exercises = _exercises.Where(e => e.Type == "HTML").ToList();
            return View(exercises);
        }

        public IActionResult Css()
        {
            var exercises = _exercises.Where(e => e.Type == "CSS").ToList();
            return View(exercises);
        }

        [HttpPost]
        public IActionResult Submit(int exerciseId, string userCode)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Id == exerciseId);
            if (exercise == null)
            {
                return NotFound();
            }

            // Simple check: compare userCode with Solution (ignoring whitespace)
            bool isCorrect = string.Equals(userCode?.Trim(), exercise.Solution?.Trim(), StringComparison.OrdinalIgnoreCase);

            var exercises = _exercises.Where(e => e.Type == "HTML").ToList();
            ViewBag.IsCorrect = isCorrect;
            ViewBag.UserCode = userCode;
            ViewBag.ResultExercise = exercise;
            return View("Html", exercises);
        }

        public IActionResult Progress()
        {
            return View();
        }
    }
}
=======
    }
}
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
=======
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EasyCode.Models;

namespace EasyCode.Controllers
{
    
    public class ExercisesController : Controller
    {
        private readonly ILogger<ExercisesController> _logger;
        private static readonly List<Exercise> _exercises = new List<Exercise>
        {
            new Exercise { Id = 1, Title = "Tạo trang HTML đầu tiên", Description = "Tạo một trang HTML đơn giản với cấu trúc cơ bản", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Trang HTML đầu tiên của tôi</title>\n</head>\n<body>\n  <h1>Xin chào thế giới!</h1>\n  <p>Đây là trang HTML đầu tiên tôi tạo bằng tay.</p>\n</body>\n</html>", Hint = "Theo yêu cầu" },
            new Exercise { Id = 2, Title = "Phân biệt các phần <head> và <body>", Description = "Tạo trang HTML với phần <head> và <body>", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Phân biệt phần head và body</title>\n</head>\n<body>\n  <h1>Đây là phần thân (body) của trang</h1>\n  <p>Phần <head> chứa thông tin về trang, còn <body> là nơi hiển thị nội dung.</p>\n</body>\n</html>", Hint = "Theo yêu cầu" },
            new Exercise { Id = 3, Title = "Thêm ký tự đặc biệt", Description = "Tạo trang HTML hiển thị các ký tự đặc biệt", Type = "HTML", Code = "", Solution = "<!DOCTYPE html>\n<html lang=\"vi\">\n<head>\n  <meta charset=\"UTF-8\">\n  <title>Ký tự đặc biệt trong HTML</title>\n</head>\n<body>\n  <h1>Các ký tự đặc biệt trong HTML</h1>\n  <p>Bản quyền: &copy; 2025 EasyCode</p>\n  <p>Thương hiệu: EasyCode&trade;</p>\n  <p>Ký hiệu nhỏ hơn: &lt; và lớn hơn: &gt;</p>\n  <p>Trái tim: &hearts;</p>\n</body>\n</html>", Hint = "Sử dụng entity codes" }
        };

        public ExercisesController(ILogger<ExercisesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Html()
        {
            var exercises = _exercises.Where(e => e.Type == "HTML").ToList();
            return View(exercises);
        }

        public IActionResult Css()
        {
            var exercises = _exercises.Where(e => e.Type == "CSS").ToList();
            return View(exercises);
        }

        [HttpPost]
        public IActionResult Submit(int exerciseId, string userCode)
        {
            var exercise = _exercises.FirstOrDefault(e => e.Id == exerciseId);
            if (exercise == null)
            {
                return NotFound();
            }

            // Simple check: compare userCode with Solution (ignoring whitespace)
            bool isCorrect = string.Equals(userCode?.Trim(), exercise.Solution?.Trim(), StringComparison.OrdinalIgnoreCase);

            var exercises = _exercises.Where(e => e.Type == "HTML").ToList();
            ViewBag.IsCorrect = isCorrect;
            ViewBag.UserCode = userCode;
            ViewBag.ResultExercise = exercise;
            return View("Html", exercises);
        }

        public IActionResult Progress()
        {
            return View();
        }
    }
}
>>>>>>> d378627 (Initial commit)
