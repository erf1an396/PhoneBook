using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.CoreLayer.Services.Contacts;
using PhoneBook.CoreLayer.Services.Users;
using PhoneBook.DataLayer.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhoneBook.CoreLayer.Services.DbInitializer;
using PhoneBook.CoreLayer.Services.Roles;
using PhoneBook.CoreLayer.Services.Users.UserShowService;
using PhoneBook.CoreLayer.Utilities;
using PhoneBook.DataLayer.Entities;
using PhoneBook.CoreLayer.DTOs.Users;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
        sqlOptions => sqlOptions.MigrationsAssembly("PhoneBook.DataLayer")));





builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserShowService, UserShowService>();

builder.Services.AddScoped<IDBInitialize, DBInitialize>();






builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        // User Options
        //options.User.RequireUniqueEmail = true;
        //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
        // Signin Options
        //options.SignIn.RequireConfirmedEmail = false;
        //options.SignIn.RequireConfirmedPhoneNumber = true;
        // Password Options
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
        // LockOut
        options.Lockout.AllowedForNewUsers = false;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
        options.Lockout.MaxFailedAccessAttempts = 3;
        // Stores Options
        //options.Stores.MaxLengthForKeys = 10;
        options.Stores.ProtectPersonalData = false;

        //options.Tokens.AuthenticatorTokenProvider = "";

        //options.ClaimsIdentity.UserNameClaimType = "ClaimTypes.Name";
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
.AddErrorDescriber<PersianIdentityErrors>();





builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/LogOut";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
});


//builder.Services.AddAuthentication(option =>
//{
//    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(option =>
//{
//    option.LoginPath = "/auth/login";
//    option.LogoutPath = "/Auth/Logout";
//    option.ExpireTimeSpan = TimeSpan.FromDays(30);
//});

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Admin", policy => policy.RequireRole("admin"));
    option.AddPolicy("User", policy => policy.RequireRole("user"));
    option.AddPolicy("Writer", policy => policy.RequireRole("writer"));
    option.AddPolicy("AddContact", policy => policy.RequireRole("AddContact"));
    option.AddPolicy("UpdateContact", policy => policy.RequireRole("DeleteContact"));
    option.AddPolicy("Product", policy => policy.RequireRole("Product"));
    option.AddPolicy("AddRole", policy => policy.RequireRole("AddRole"));
    option.AddPolicy("DeleteRole", policy => policy.RequireRole("DeleteRole"));
    option.AddPolicy("DeleteUser", policy => policy.RequireRole("DeleteUser"));
    option.AddPolicy("UpdateUser", policy => policy.RequireRole("UpdateUser"));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.RunDatabaseInitializer();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication();

app.UseAuthorization();





app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
