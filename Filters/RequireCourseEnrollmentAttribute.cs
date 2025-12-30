using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EasyCode.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace EasyCode.Filters
{
    public sealed class RequireCourseEnrollmentAttribute : TypeFilterAttribute
    {
        public RequireCourseEnrollmentAttribute() : base(typeof(RequireCourseEnrollmentFilter))
        {
        }
    }

    public sealed class RequireCourseEnrollmentFilter : IAsyncAuthorizationFilter
    {
        private readonly DataContext _db;
        private readonly ITempDataDictionaryFactory _tempDataFactory;

        public RequireCourseEnrollmentFilter(DataContext db, ITempDataDictionaryFactory tempDataFactory)
        {
            _db = db;
            _tempDataFactory = tempDataFactory;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return;
            }

            var email = user.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrWhiteSpace(email))
            {
                DenyWithMessage(context);
                return;
            }

            var enrollment = await _db.Enrollments.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
            if (enrollment == null)
            {
                context.HttpContext.Session.SetInt32("HasCourse", 0);
                context.HttpContext.Session.Remove("EnrollmentId");

                DenyWithMessage(context);
                return;
            }

            context.HttpContext.Session.SetInt32("HasCourse", 1);
            context.HttpContext.Session.SetInt32("EnrollmentId", enrollment.EnrollmentId);
        }

        private void DenyWithMessage(AuthorizationFilterContext context)
        {
            const string message = "Bạn phải đăng ký khóa học trước khi vào Lesson hoặc Exercises.";

            var request = context.HttpContext.Request;
            var wantsJson = request.Headers.Accept.ToString().Contains("application/json", StringComparison.OrdinalIgnoreCase)
                || string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);

            if (wantsJson)
            {
                context.Result = new JsonResult(new { success = false, message }) { StatusCode = StatusCodes.Status403Forbidden };
                return;
            }

            var tempData = _tempDataFactory.GetTempData(context.HttpContext);
            tempData["CourseRequired"] = message;

            context.Result = new RedirectToActionResult("DangKy", "Home", routeValues: null);
        }
    }
}
