global using FYP5.Models;
global using RP.SOI.DotNet.Utils;
global using System.Data;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// authentication
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


