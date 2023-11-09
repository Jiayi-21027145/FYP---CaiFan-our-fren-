using Microsoft.AspNetCore.Mvc;
using FYP3.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using RP.SOI.DotNet.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FYP3.Controllers;

public class AccountController : Controller
{
    private const string REDIRECT_CNTR = "Home";
    private const string REDIRECT_ACTN = "GiftShop";
    private const string LOGIN_VIEW = "Login";
    private readonly AppDbContext _dbCtx;
    private readonly IDbService _dbSvc;
    private readonly IAuthService _authSvc;

    public AccountController(IDbService dbSvc, IAuthService authSvc, AppDbContext dbCtx)
    {
        _dbSvc = dbSvc;
        _authSvc = authSvc;
        _dbCtx = dbCtx;
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null!)
    {
        TempData["ReturnUrl"] = returnUrl;
        return View(LOGIN_VIEW);
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(UserLogin user)
    {
        const string sqlLogin =
            @"SELECT UserID, Username FROM Users 
               WHERE UserID = '{0}' 
                 AND Password = HASHBYTES('SHA1', '{1}')";


        if (!_authSvc.Authenticate(sqlLogin, user.UserID, user.Password,
                                   out ClaimsPrincipal? principal))
        {
            ViewData["Message"] = "Incorrect Email or Password";
            ViewData["MsgType"] = "warning";
            return View(LOGIN_VIEW);
        }
        else
        {
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme, // Default Scheme
               principal!,
               new AuthenticationProperties
               {
                   IsPersistent = true
               });

            if (TempData["returnUrl"] != null)
            {
                string returnUrl = TempData["returnUrl"]!.ToString()!;
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
            }

            return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
        }
    }

    [Authorize]
    public IActionResult Logoff(string returnUrl = null!)
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction(REDIRECT_ACTN, REDIRECT_CNTR);
    }

    [AllowAnonymous]
    public IActionResult Forbidden()
    {
        return View();
    }
    [AllowAnonymous]
    public IActionResult SignUp()
    {
        return View("SignUp");
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult SignUp(Users usr)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Check if the user already exists
                if (_dbCtx.Users.Any(u => u.UserID == usr.UserID))
                {
                    ViewData["Message"] = "User already exists with this UserID.";
                    ViewData["MsgType"] = "danger";
                }
                else
                {
                    // You should hash the password before saving it to the database
                    // Consider using a library like BCrypt to securely hash passwords
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(usr.Password);

                    // Insert the new user into the database
                    _dbCtx.Users.Add(new Users
                    {
                        UserID = usr.UserID,
                        Username = usr.Username,
                        Email = usr.Email,
                        Password = hashedPassword,
                        Gender = usr.Gender,
                        Reference = usr.Reference
                    });

                    _dbCtx.SaveChanges();

                    // Send a welcome email here (implement this)
                    // Make sure to send the unhashed password in the email, not the hashed one

                    ViewData["Message"] = "User Successfully Registered";
                    ViewData["MsgType"] = "success";
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // LogException(ex);
                Console.WriteLine("Error during user registration: " + ex.Message);

                ViewData["Message"] = "An error occurred while registering the user.";
                ViewData["MsgType"] = "danger";
            }
        }
        else
        {
            // Display validation errors
            ViewData["Message"] = "Please correct the errors in the form.";
            ViewData["MsgType"] = "danger";
        }

        return View("SignUp");

    }
}
