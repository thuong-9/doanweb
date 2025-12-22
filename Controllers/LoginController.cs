using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using EasyCode.Models;
using EasyCode.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EasyCode.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DataContext _db;
        private readonly PasswordHasher<Users> _passwordHasher;

        public LoginController(ILogger<LoginController> logger, DataContext db)
        {
            _logger = logger;
            _db = db;
            _passwordHasher = new PasswordHasher<Users>();
        }

        [HttpGet]
        public IActionResult Index(string? mode = null, string? returnUrl = null)
        {
            var isSignup = string.Equals(mode, "signup", StringComparison.OrdinalIgnoreCase);

            var vm = new LoginPageViewModel
            {
                IsSignup = isSignup,
                Login = { ReturnUrl = returnUrl },
                Register = { ReturnUrl = returnUrl }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind(Prefix = "Register")] RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new LoginPageViewModel
                {
                    IsSignup = true,
                    Register = register,
                    Login = new LoginViewModel { ReturnUrl = register.ReturnUrl }
                });
            }

            var normalizedEmail = register.Email.Trim();
            var existing = await _db.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == normalizedEmail);

            if (existing != null)
            {
                ModelState.AddModelError("Register.Email", "Email already exists.");
                return View("Index", new LoginPageViewModel
                {
                    IsSignup = true,
                    Register = register,
                    Login = new LoginViewModel { ReturnUrl = register.ReturnUrl }
                });
            }

            var user = new Users
            {
                UserName = register.UserName.Trim(),
                Email = normalizedEmail,
            };
            user.Password = _passwordHasher.HashPassword(user, register.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            await SignInUserAsync(user);

            if (!string.IsNullOrWhiteSpace(register.ReturnUrl) && Url.IsLocalUrl(register.ReturnUrl))
            {
                return Redirect(register.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn([Bind(Prefix = "Login")] LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new LoginPageViewModel
                {
                    IsSignup = false,
                    Login = login,
                    Register = new RegisterViewModel { ReturnUrl = login.ReturnUrl }
                });
            }

            var email = login.Email.Trim();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                ModelState.AddModelError("Login.Email", "Invalid email or password.");
                return View("Index", new LoginPageViewModel
                {
                    IsSignup = false,
                    Login = login,
                    Register = new RegisterViewModel { ReturnUrl = login.ReturnUrl }
                });
            }

            var verify = _passwordHasher.VerifyHashedPassword(user, user.Password ?? string.Empty, login.Password);
            if (verify == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Login.Email", "Invalid email or password.");
                return View("Index", new LoginPageViewModel
                {
                    IsSignup = false,
                    Login = login,
                    Register = new RegisterViewModel { ReturnUrl = login.ReturnUrl }
                });
            }

            await SignInUserAsync(user);

            if (!string.IsNullOrWhiteSpace(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
            {
                return Redirect(login.ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [ActionName("Logout")]
        public async Task<IActionResult> LogoutGet()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUserAsync(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? "User"),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                });
        }

    }
}