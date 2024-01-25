global using FYP5.Models;
global using FYP5.Services;
global using RP.SOI.DotNet.Utils;
global using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// authentication
builder.Services.AddDbContext<AppDbContext>(
   options => options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services
.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Forbidden";
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


