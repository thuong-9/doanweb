using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
<<<<<<< HEAD
=======
using System.Security.Cryptography;
using System.IO;
using Microsoft.AspNetCore.WebUtilities;
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
using EasyCode.Models;
using EasyCode.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Hosting;
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f

namespace EasyCode.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly DataContext _db;
        private readonly PasswordHasher<Users> _passwordHasher;
<<<<<<< HEAD

        public LoginController(ILogger<LoginController> logger, DataContext db)
=======
        private readonly IWebHostEnvironment _env;

        public LoginController(ILogger<LoginController> logger, DataContext db, IWebHostEnvironment env)
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
        {
            _logger = logger;
            _db = db;
            _passwordHasher = new PasswordHasher<Users>();
<<<<<<< HEAD
=======
            _env = env;
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
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

<<<<<<< HEAD
=======
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Submitted = false;
                return View(vm);
            }

            var email = vm.Email.Trim();

            // Always generate a token so the response doesn't leak whether the email exists.
            var rawToken = WebEncoders.Base64UrlEncode(RandomNumberGenerator.GetBytes(32));
            var tokenHash = ComputeSha256Base64(rawToken);

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                _logger.LogInformation("Forgot password requested for {Email}", email);

                user.PasswordResetTokenHash = tokenHash;
                user.PasswordResetTokenExpiresUtc = DateTime.UtcNow.AddMinutes(30);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("ResetPassword", new { email, token = rawToken });
        }

        [HttpGet]
        public IActionResult ResetPassword(string? email = null, string? token = null)
        {
            return View(new ResetPasswordViewModel
            {
                Email = email ?? string.Empty,
                Token = token ?? string.Empty,
                Submitted = false
            });
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string? message = null)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Index");
            }

            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out var userId))
            {
                return RedirectToAction("Index");
            }

            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var displayName = string.IsNullOrWhiteSpace(user.UserName) ? (User.Identity?.Name ?? "User") : user.UserName;
            var firstChar = displayName.Trim().Length > 0 ? displayName.Trim()[0].ToString().ToUpperInvariant() : "U";

            return View(new ProfilePageViewModel
            {
                AvatarUrl = string.IsNullOrWhiteSpace(user.AvatarPath) ? string.Empty : user.AvatarPath,
                AvatarText = firstChar,
                StatusMessage = message,
                Update = new ProfileUpdateViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email,
                },
                Password = new ChangePasswordViewModel()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile([Bind(Prefix = "Update")] ProfileUpdateViewModel update)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Index");
            }

            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out var userId))
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return await RenderProfileWithErrorsAsync(userId, update: update);
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var newEmail = update.Email.Trim();
            var emailTaken = await _db.Users.AsNoTracking()
                .AnyAsync(u => u.UserID != userId && u.Email == newEmail);
            if (emailTaken)
            {
                ModelState.AddModelError("Update.Email", "Email already exists.");
                return await RenderProfileWithErrorsAsync(userId, update: update);
            }

            user.UserName = update.UserName.Trim();
            user.Email = newEmail;

            if (update.AvatarFile != null && update.AvatarFile.Length > 0)
            {
                var contentType = update.AvatarFile.ContentType?.ToLowerInvariant() ?? string.Empty;
                if (!contentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("Update.AvatarFile", "Please upload an image file.");
                    return await RenderProfileWithErrorsAsync(userId, update: update);
                }

                // 2MB limit
                if (update.AvatarFile.Length > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("Update.AvatarFile", "Image must be <= 2MB.");
                    return await RenderProfileWithErrorsAsync(userId, update: update);
                }

                var ext = Path.GetExtension(update.AvatarFile.FileName);
                if (string.IsNullOrWhiteSpace(ext))
                {
                    ext = ".jpg";
                }

                var uploadsDir = Path.Combine(_env.WebRootPath, "uploads", "avatars");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"u{userId}_{Guid.NewGuid():N}{ext}";
                var fullPath = Path.Combine(uploadsDir, fileName);

                using (var stream = System.IO.File.Create(fullPath))
                {
                    await update.AvatarFile.CopyToAsync(stream);
                }

                user.AvatarPath = $"/uploads/avatars/{fileName}";
            }

            await _db.SaveChangesAsync();
            await SignInUserAsync(user);

            return RedirectToAction("Profile", new { message = "Đã lưu thay đổi." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([Bind(Prefix = "Password")] ChangePasswordViewModel password)
        {
            if (User?.Identity?.IsAuthenticated != true)
            {
                return RedirectToAction("Index");
            }

            var idClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(idClaim, out var userId))
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return await RenderProfileWithErrorsAsync(userId, password: password);
            }

            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var verify = _passwordHasher.VerifyHashedPassword(user, user.Password ?? string.Empty, password.CurrentPassword);
            if (verify == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError("Password.CurrentPassword", "Current password is incorrect.");
                return await RenderProfileWithErrorsAsync(userId, password: password);
            }

            user.Password = _passwordHasher.HashPassword(user, password.NewPassword);
            await _db.SaveChangesAsync();

            return RedirectToAction("Profile", new { message = "Đã cập nhật mật khẩu." });
        }

        private async Task<IActionResult> RenderProfileWithErrorsAsync(int userId, ProfileUpdateViewModel? update = null, ChangePasswordViewModel? password = null)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var displayName = string.IsNullOrWhiteSpace(user.UserName) ? "User" : user.UserName;
            var firstChar = displayName.Trim().Length > 0 ? displayName.Trim()[0].ToString().ToUpperInvariant() : "U";

            return View("Profile", new ProfilePageViewModel
            {
                AvatarUrl = string.IsNullOrWhiteSpace(user.AvatarPath) ? string.Empty : user.AvatarPath,
                AvatarText = firstChar,
                Update = update ?? new ProfileUpdateViewModel { UserName = user.UserName, Email = user.Email },
                Password = password ?? new ChangePasswordViewModel()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Submitted = false;
                return View(vm);
            }

            var email = vm.Email.Trim();
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid or expired reset link.");
                vm.Submitted = false;
                return View(vm);
            }

            if (user.PasswordResetTokenExpiresUtc == null || user.PasswordResetTokenExpiresUtc < DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, "Invalid or expired reset link.");
                vm.Submitted = false;
                return View(vm);
            }

            var providedHash = ComputeSha256Base64(vm.Token);
            if (string.IsNullOrWhiteSpace(user.PasswordResetTokenHash) || user.PasswordResetTokenHash != providedHash)
            {
                ModelState.AddModelError(string.Empty, "Invalid or expired reset link.");
                vm.Submitted = false;
                return View(vm);
            }

            user.Password = _passwordHasher.HashPassword(user, vm.NewPassword);
            user.PasswordResetTokenHash = null;
            user.PasswordResetTokenExpiresUtc = null;

            await _db.SaveChangesAsync();

            return View(new ResetPasswordViewModel
            {
                Email = email,
                Token = string.Empty,
                Submitted = true
            });
        }

        private static string ComputeSha256Base64(string input)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
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
<<<<<<< HEAD
=======
                new Claim("avatar", user.AvatarPath ?? string.Empty),
>>>>>>> bf3e2edcc384baa954f85aabdc33b5eaf544c18f
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