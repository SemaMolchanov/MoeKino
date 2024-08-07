using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using MoeKinoWebApp.Data;
using Microsoft.AspNetCore.Identity;
using MoeKinoWebApp.Models;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddSingleton<PasswordHasher<User>>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; 
        options.LogoutPath = "/Auth/Logout"; 
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); 
        options.SlidingExpiration = true; 
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireRole("Admin"));
});


builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapAreaControllerRoute(
    name: "admin_area",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "auth_area",
    areaName: "Auth",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapAreaControllerRoute(
    name: "admin_area_with_keys",
    areaName: "Admin",
    pattern: "{area:exists}/{controller}/{action}/{movieID}/{genreID}");


app.MapAreaControllerRoute(
    name: "main_area",
    areaName: "Main",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.Run();
