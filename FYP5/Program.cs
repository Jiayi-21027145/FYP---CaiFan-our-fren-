global using FYP5.Models;
global using FYP5.Services;
global using RP.SOI.DotNet.Utils;
global using System.Data;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authorization;
global using RP.SOI.DotNet.Services;
global using Microsoft.AspNetCore.Mvc.Rendering;
global using System.ComponentModel.DataAnnotations;
global using System.Security.Claims;
global using System.Dynamic;
using Microsoft.AspNetCore.Authentication.Cookies;
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
builder.Services.AddScoped<IDbService, DbService>();


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


