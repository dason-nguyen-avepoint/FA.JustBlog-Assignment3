using FA.JustBlog.Core.Data;
using FA.JustBlog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using FA.JustBlog.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;
using FA.JustBlog.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//Add connection data service
builder.Services.AddDbContext<ApplicationDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("JustBlogConnection")));

//builder.Services.AddDbContext<ApplicationDbContext1>(option =>
//    option.UseSqlServer(builder.Configuration.GetConnectionString("JustBlogConnection")));

// DEFAULT IDENTITY
// builder.Services.AddDefaultIdentity<IdentityUser>(
// options => options.SignIn.RequireConfirmedAccount = true)
// .AddEntityFrameworkStores<ApplicationDbContext>();

// SETTINGS ROLE IDENTITY
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

//CONFIGURE COOKIE
builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = $"/Identity/Account/Login";
    option.LogoutPath = $"/Identity/Account/Logout";
    option.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});


// CONFIG EMAIL SENDER
builder.Services.AddScoped<IEmailSender, EmailSender>();

//SETTING JSON FILE
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// IDENTITY OPTIONS
builder.Services.Configure<IdentityOptions>(options =>
{
    // CONFIGURE PASSWORD
    options.Password.RequireDigit = false; // DO NOT REQUIRE NUMBER IN PASSWORD
    options.Password.RequireLowercase = false; // DO NOT REQUIRE LOWER CHARACTERS IN PASSWORD
    options.Password.RequireNonAlphanumeric = false; // DO NOT REQUIRE SPECIAL CHARACTER (!@#$%...)
    options.Password.RequireUppercase = false; // DO NOT REQUIRE UPPER CHARACTERS IN PASSWORD
    options.Password.RequiredLength = 6; // MINIMUN LENGTH OF PASSWORD IS ...
    options.Password.RequiredUniqueChars = 1; // NUMBER OF UNIQUE CHARACTER

    // CONFIGURE LOCKOUT - LOCKUSER
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // TIME OF LOCK USER WHEN LOGIN FAIL
    options.Lockout.MaxFailedAccessAttempts = 5; // NUMBER OF FAILED LOGIN
    options.Lockout.AllowedForNewUsers = true;

    // CONFIGURE USER
    //options.User.AllowedUserNameCharacters =
    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-.@+"; // ALLOW USER NAME HAS THIS CHARACTERS
    options.User.RequireUniqueEmail = true; // EMAIL IS UNIQUE

    //CONFIGURE LOGIN
    options.SignIn.RequireConfirmedEmail = true; // CONFIRM EMAIL
    options.SignIn.RequireConfirmedPhoneNumber = false; // DO NOT CONFIRM PHONE NUMBER


});

var app = builder.Build();

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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    areaName :"Admin",
    name: "Admin",
    pattern: "{area=Admin}/{controller=Posts}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    areaName: "Admin",
    name: "sortBy",
    pattern: "{area=Admin}/{controller=Posts}/{action=Index}/{sortBy}");
app.MapAreaControllerRoute(
    areaName: "Admin",
    name: "sortBy1",
    pattern: "{area=Admin}/{controller=Posts}/{sortBy?}",
    defaults: new {controller = "Posts",action = "GetPosts"}
    );
app.MapControllerRoute(
    name: "post",
    pattern: "{controler=Posts}/{year}/{month}/{title}",
    defaults: new { controller ="Posts", action="Details"},
    constraints: new { year = @"\d{4}", month = @"\d{2}" }
    );
app.Run();
