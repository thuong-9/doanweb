using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EasyCode.Models;

namespace EasyCode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
    public ActionResult DangKy()
    {
        return View();
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpPost]
    public IActionResult GuiOTP(string sdt)
    {
        var otp = new Random().Next(100000, 999999).ToString();
        HttpContext.Session.SetString("OTP", otp);

        // giả lập gửi SMS
        Console.WriteLine($"OTP gửi tới {sdt}: {otp}");

        return Json(new { success = true });
    }
    [HttpPost]
    public IActionResult XacNhanOTP(string otp)
    {
        var realOtp = HttpContext.Session.GetString("OTP");

        if (otp == realOtp)
            return Json(new { success = true });

        return Json(new { success = false });
    }
}