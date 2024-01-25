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
    private readonly AppDbContext _dbCtx;
    public AccountController( AppDbContext dbCtx)
    {
        
        _dbCtx = dbCtx;
    }

    
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
    public IActionResult ForgotPassword()
    {
        // Just return the view which contains the form for user to submit the userID
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public IActionResult ForgotPassword(string userId)
    {
        // Verify if the userID is in the database
        string select = $"SELECT * FROM JiakUser WHERE UserId = '{userId}'";
        DataTable dt = DBUtl.GetTable(select);

        // Check if the UserID exists
        if (dt.Rows.Count > 0)
        {
            // UserID exists, redirect them to the Reset Password page
            // Pass the UserID along if needeaswword, but ensure it's done securely
            return RedirectToAction("ResetPassword", new { id = userId });
        }
        else
        {
            // UserID does not exist, show an error message
            ViewData["Message"] = "User ID does not exist.";
            ViewData["MsgType"] = "danger";
            return View();
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult ResetPassword(string id)
    {
        ViewData["UserId"] = id;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult ResetPassword(Password model, string id)
    {
        try
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                ViewData["UserId"] = model.UserId;
                return View(model);
            }

            // Update the password in the database
            id = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            string updateSql = "UPDATE JiakUser SET UserPw = HASHBYTES('SHA1', @p1) WHERE UserId = @p0";
            int result = DBUtl.ExecSQL(updateSql, id, model.NewPassword);

            if (result == 1)
            {
                TempData["Message"] = "Your password has been updated successfully.";
                TempData["MsgType"] = "success";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                TempData["Message"] = "Error updating password: " + DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
                ViewData["UserId"] = model.UserId;
                return View(model);
            }
        }
        catch (Exception ex)
        {
            TempData["Message"] = "An error occurred: " + ex.Message;
            TempData["MsgType"] = "danger";
            ViewData["UserId"] = model.UserId;
            return View(model);
        }
    }

    [Authorize]
    public IActionResult ChangePwd() 
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult ChangePwd(ChangePw pwd)
    {
        var userid = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userid == null)
        {
            ViewData["Msg"] = "User not found.";
            return View();
        }

        string updateSql = @"
        UPDATE AppUser
        SET UserPass = HASHBYTES('SHA1', CONVERT(VARCHAR, @p1))
        WHERE Id = @p0
        AND UserPass = HASHBYTES('SHA1', CONVERT(VARCHAR, @p2))";

        int result = DBUtl.ExecSQL(updateSql, userid, pwd.NewPwd, pwd.CurrentPwd);

        if (result == 1)
            ViewData["Msg"] = "Password Updated";
        else
            ViewData["Msg"] = "Failed to Update Password";

        return View();
    }

    [Authorize]
    public JsonResult VerifyCurrentPassword(string CurrentPwd)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string sql = @"
        SELECT * FROM AppUser
        WHERE Id = @0 AND UserPass = HASHBYTES('SHA1', CONVERT(VARCHAR, @1))";

        var users = DBUtl.GetList<JiakUser>(sql, userid, CurrentPwd);

        if (users.Any())
            return Json(true);
        else
            return Json(false);
    }

    [Authorize]
    public JsonResult VerifyNewPassword(string CurrentPwd)
    {
        string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        string sql = @"
        SELECT * FROM AppUser
        WHERE Id = @0 AND UserPass = HASHBYTES('SHA1', CONVERT(VARCHAR, @1))";

        var users = DBUtl.GetList<JiakUser>(sql, userid, CurrentPwd);

        if (users.Any())
            return Json(true);
        else
            return Json(false);
    }






    //return RedirectToAction("Login");
    /*string userid = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

    string select = @"SELECT UserPw FROM JiakUser 
                     WHERE UserId='{0}'";
    string sql = string.Format(select, userid);
    List<JiakUser> user = DBUtl.GetList<JiakUser>(sql);
    if (user.Count == 1)
    {
        string update = @"UPDATE JiakUser  
                          SET UserPw=HASHBYTES('SHA1', '{pw.NewPassword}') WHERE UserId={0} AND UserPw={1}";

        string sql2 = string.Format(update, pw.NewPassword);

        if (DBUtl.ExecSQL(sql2) == 1)
        {
            TempData["Message"] = "Password Updated";
            TempData["MsgType"] = "success";
            return View("Login");
        }
        else
        {
            TempData["Message"] = DBUtl.DB_Message;
            ViewData["ExecSQL"] = DBUtl.DB_SQL;
            TempData["MsgType"] = "danger";  
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

    

        // Use the string ID to retrieve only specific columns for the user from the database
        var userProjection = _dbCtx.JiakUser
            .Where(u => u.UserId == userIdClaim.Value)
            .Select(u => new JiakUser
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                Gender = u.Gender
                // Do not include other properties like Password, UserRole, etc.
            })
            .FirstOrDefault();

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



