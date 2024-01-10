using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FYP5.Models;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace FYP5.Controllers;

public class AccountController : Controller
{
    private const string LOGIN_SQL =
       @"SELECT * FROM JiakUser 
            WHERE UserId = '{0}' 
              AND UserPw = HASHBYTES('SHA1', '{1}')";

    private const string LASTLOGIN_SQL =
       @"UPDATE JiakUser SET LastLogin=GETDATE() WHERE UserId='{0}'";

    private const string ROLE_COL = "UserRole";
    private const string NAME_COL = "UserName";

    private const string REDIRECT_CNTR = "Home";
    private const string REDIRECT_ACTN = "Index";

    private const string LOGIN_VIEW = "Login";

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
        if (!AuthenticateUser(user.UserID, user.Password,
                              out ClaimsPrincipal principal))
        {
            ViewData["Message"] = "Incorrect User ID or Password";
            ViewData["MsgType"] = "warning";
            return View(LOGIN_VIEW);
        }
        else
        {
            HttpContext.SignInAsync(
               CookieAuthenticationDefaults.AuthenticationScheme,
               principal,
               new AuthenticationProperties
               {
                   IsPersistent = user.RememberMe
               });

            // Update the Last Login Timestamp of the User
            DBUtl.ExecSQL(LASTLOGIN_SQL, user.UserID);

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
            string insert = @"INSERT INTO JiakUser(UserId, UserPw, UserName, Gender, Email, UserRole) VALUES
                 ('{0}', HASHBYTES('SHA1', '{1}'), '{2}', '{3}', '{4}', 'User')";
            if (DBUtl.ExecSQL(insert, usr.UserId, usr.UserPw, usr.UserName, usr.Gender[..1], usr.Email) == 1)
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
            return View("Login");
        }
    }
    [AllowAnonymous]
    public IActionResult VerifyUserID(string userId)
    {
        string select = $"SELECT * FROM JiakUser WHERE UserId='{userId}'";
        if (DBUtl.GetTable(select).Rows.Count > 0)
        {
            return Json($"'{userId}' already in use");
        }
        return Json(true);
    }
    private static bool AuthenticateUser(string uid, string pw,
                                         out ClaimsPrincipal principal)
    {
        principal = null!;

        // TODO: Lesson09 Task 1 - Make login secure, use the new way of calling DBUtl
        //string select = string.Format(sql, uid, pw);
        DataTable ds = DBUtl.GetTable(LOGIN_SQL, uid, pw);
        if (ds.Rows.Count == 1)
        {
            principal =
               new ClaimsPrincipal(
                  new ClaimsIdentity(
                     new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, uid),
                        new Claim(ClaimTypes.Name, ds.Rows[0][NAME_COL]!.ToString()!),
                        new Claim(ClaimTypes.Role, ds.Rows[0][ROLE_COL]!.ToString()!),
                     }, "Basic"
                     )
                  );
            return true;
        }
        return false;
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }

    public IActionResult ResetPW()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ResetPW(Password pw)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string select = @"SELECT UserPw FROM JiakUser 
                         WHERE Id={0} AND UserId='{1}'";
        string sql = string.Format(select, userid);
        List<JiakUser> user = DBUtl.GetList<JiakUser>(sql);
        if (user.Count == 1)
        {
            string update = @"UPDATE TravelHighlight  
                              SET UserPw='{1}' WHERE UserId={0}";

            string sql2 = string.Format(update, userid, pw.NewPassword);

            if (DBUtl.ExecSQL(sql2) == 1)
            {
                TempData["Message"] = "Password Updated";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                ViewData["ExecSQL"] = DBUtl.DB_SQL;
                TempData["MsgType"] = "danger";
                return RedirectToAction("ResetPW");
            }
        }
        else
        {
            TempData["Message"] = "User Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("ResetPW");
        }
        return View();
    }

    


public IActionResult UpdateProfile(int id)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string select = @"SELECT * FROM JiaUser 
                         WHERE Id={0} AND UserId='{1}'";

        // TODO: Lesson09 Task 2c - Make insecure DB SELECT secure.
        string sql = string.Format(select, id, userid);
        List<JiakUser> lstTrip = DBUtl.GetList<JiakUser>(select, id, userid);
        if (lstTrip.Count == 1)
        {
            JiakUser trip = lstTrip[0];
            return View(trip);
        }
        else
        {
            TempData["Message"] = "Trip Record does not exist";
            TempData["MsgType"] = "warning";
            return RedirectToAction("MyTrips");
        }
    }
}



