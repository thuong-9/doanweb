using EasyCode.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
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

-- Ensure Attendances table exists (EnsureCreated won't add tables to an existing DB)
IF OBJECT_ID(N'dbo.Attendances', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Attendances
    (
        [AttendanceId] INT IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Attendances] PRIMARY KEY,
        [EnrollmentId] INT NOT NULL,
        [AttendanceDate] DATE NOT NULL,
        [IsPresent] BIT NOT NULL CONSTRAINT [DF_Attendances_IsPresent] DEFAULT(1)
    );

    ALTER TABLE dbo.Attendances WITH CHECK
    ADD CONSTRAINT [FK_Attendances_Enrollments_EnrollmentId]
        FOREIGN KEY([EnrollmentId]) REFERENCES dbo.Enrollments([EnrollmentId])
        ON DELETE CASCADE;
END
ELSE
BEGIN
    -- Backfill schema if the table exists but is missing newer columns
    IF COL_LENGTH(N'dbo.Attendances', N'IsPresent') IS NULL
    BEGIN
        EXEC(N'ALTER TABLE dbo.Attendances ADD [IsPresent] BIT NOT NULL CONSTRAINT [DF_Attendances_IsPresent] DEFAULT(1)');
    END
END

-- Unique attendance per enrollment per day
IF OBJECT_ID(N'dbo.Attendances', N'U') IS NOT NULL
    AND NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE [name] = N'IX_Attendances_EnrollmentId_AttendanceDate'
          AND [object_id] = OBJECT_ID(N'dbo.Attendances')
    )
BEGIN
    CREATE UNIQUE INDEX [IX_Attendances_EnrollmentId_AttendanceDate]
        ON dbo.Attendances([EnrollmentId], [AttendanceDate]);
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
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

