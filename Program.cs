using EasyCode.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EasyCode.Models.DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromDays(14);
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Ensure database exists (simple setup without migrations)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
    try
    {
        var db = services.GetRequiredService<EasyCode.Models.DataContext>();
        db.Database.EnsureCreated();

        // If the table already exists with short VARCHAR/NVARCHAR columns (common in manual DB setups),
        // widen them so EF can store hashed passwords without truncation.
        db.Database.ExecuteSqlRaw(@"
IF OBJECT_ID(N'dbo.Users', N'U') IS NOT NULL
BEGIN
    -- Password hashes can exceed 100 chars; ensure sufficient size
    IF COL_LENGTH(N'dbo.Users', N'Password') IS NOT NULL
        EXEC(N'ALTER TABLE dbo.Users ALTER COLUMN [Password] NVARCHAR(512) NOT NULL');

    IF COL_LENGTH(N'dbo.Users', N'Email') IS NOT NULL
        EXEC(N'ALTER TABLE dbo.Users ALTER COLUMN [Email] NVARCHAR(256) NOT NULL');

    IF COL_LENGTH(N'dbo.Users', N'UserName') IS NOT NULL
        EXEC(N'ALTER TABLE dbo.Users ALTER COLUMN [UserName] NVARCHAR(256) NOT NULL');

    IF COL_LENGTH(N'dbo.Users', N'PasswordResetTokenHash') IS NULL
        EXEC(N'ALTER TABLE dbo.Users ADD [PasswordResetTokenHash] NVARCHAR(256) NULL');

    IF COL_LENGTH(N'dbo.Users', N'PasswordResetTokenExpiresUtc') IS NULL
        EXEC(N'ALTER TABLE dbo.Users ADD [PasswordResetTokenExpiresUtc] DATETIME2 NULL');

    IF COL_LENGTH(N'dbo.Users', N'AvatarPath') IS NULL
        EXEC(N'ALTER TABLE dbo.Users ADD [AvatarPath] NVARCHAR(260) NULL');
END
");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

