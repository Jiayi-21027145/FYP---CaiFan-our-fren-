global using FYP5.Models;
global using RP.SOI.DotNet.Utils;
global using System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

/*using CognitiveServices;

var customVision = new CustomVision();
var imagePath = "image.jpg";

var objects = customVision.DetectObjects(imagePath);

Console.WriteLine("Object - Probability - Position(X,Y)");
foreach (var obj in objects)
{
	Console.WriteLine("{0}: {1} - ({2},{3})",
		obj.TagName,
		obj.Probability,
		obj.BoundingBox.Left,
		obj.BoundingBox.Top);
}*/
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


