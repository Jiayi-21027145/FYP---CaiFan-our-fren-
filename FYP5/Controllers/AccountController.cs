using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FYP5.Controllers;

public class AccountController : Controller
{
    [Authorize]
    public IActionResult Logoff(string returnUrl = null!)
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null!)
    {
        TempData["ReturnUrl"] = returnUrl;
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Login(UserLogin user)
    {
        if (!AuthenticateUser(user.UserID, user.Password,
                              out ClaimsPrincipal principal))
        {
            ViewData["Message"] = "Incorrect User ID or Password";
            return View();
        }
        else
        {
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               principal);

            // Update the Last Login Timestamp of the User
            string update = "UPDATE JiakUser SET LastLogin=GETDATE() WHERE UserId='{0}'";
            DBUtl.ExecSQL(update, user.UserID);

            if (TempData["returnUrl"] != null)
            {
                string returnUrl = TempData["returnUrl"]!.ToString()!;
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }

    [AllowAnonymous]
    public IActionResult Forbidden()
    {
        return View();
    }

    private static bool AuthenticateUser(string uid, string pw,
                                         out ClaimsPrincipal principal)
    {
        principal = null!;
        string sql = @"SELECT * FROM JiakUser 
                       WHERE UserId = '{0}' AND UserPw = HASHBYTES('SHA1', '{1}')";
        // TODO: Lesson09 Task 1 - Make login secure, use the new way of calling DBUtl
        //string select = string.Format(sql, uid, pw);
        DataTable ds = DBUtl.GetTable(sql, uid, pw);
        if (ds.Rows.Count == 1)
        {
            principal =
               new ClaimsPrincipal(
                  new ClaimsIdentity(
                     new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, uid),
                        new Claim(ClaimTypes.Name, ds.Rows[0]["UserName"]!.ToString()!),
                        new Claim(ClaimTypes.Role, ds.Rows[0]["UserRole"]!.ToString()!)
                     },
                     CookieAuthenticationDefaults.AuthenticationScheme));
            return true;
        }
        return false;
    }
    [AllowAnonymous]
    public IActionResult SignUp()
    {
        return View();
    }
    [AllowAnonymous]
    [HttpPost]
    public IActionResult SignUp(JiakUser usr)
    {
        ModelState.Remove("UserRole");     // All new users have role set to 'member'.
        if (!ModelState.IsValid)
        {
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                ViewData["Message"] += string.Format(error.ErrorMessage);
            }
            ViewData["MsgType"] = "danger";
            return View("SignUp");
        }
        else
        {

            string insert = @"INSERT INTO JiakUser(UserId, UserPw, UserName, Email, UserRole) VALUES
                 ('{0}', HASHBYTES('SHA1', '{1}'), '{2}', '{3}', 'User')";
            if (DBUtl.ExecSQL(insert, usr.UserId, usr.UserPw, usr.UserName, usr.Email) == 1)
            {
                
                string title = "Registration Successful - Welcome";
                string message = String.Format(usr.UserName, usr.UserId, usr.UserPw);

                bool outcome = !string.IsNullOrEmpty(title);
                string result = "Something went wrong.";
                if (outcome)
                {
                    ViewData["Message"] = "User Successfully Registered";
                    ViewData["MsgType"] = "success";
                }
                else
                {
                    ViewData["Message"] = result;
                    ViewData["MsgType"] = "warning";
                }
            }
            else
            {
                ViewData["Message"] = DBUtl.DB_Message;
                ViewData["MsgType"] = "danger";
            }
            return View("SignUp");
        }
    }
}
