using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using RegisterAndLoginApp.Models;
using RegisterAndLoginApp.Services;
using RegisterAndLoginApp.Utility;

namespace RegisterAndLoginApp.Controllers
{
    public class LoginController : Controller
    {
        private static Logger logger = LogManager.GetLogger("LoginAppLoggerrule");
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [CustomAuthorization]
        public IActionResult PrivateSectionMustBeLoggedIn()
        {
            return Content("I am a protected method if the proper attribute is applied to me.");
        }
        [LogActionFilter]
        public IActionResult ProcessLogin(UserModel user)
        {
           // MyLogger.GetInstance().Info("Entering the ProcessLogin method.");
           // MyLogger.GetInstance().Info("Parameter: " + user.ToString());

            SecurityService securityService = new SecurityService();

            if (securityService.IsValid(user))
            {
               // MyLogger.GetInstance().Info("Login success.");

                HttpContext.Session.SetString("username", user.UserName);

                return View("LoginSuccess", user);
            }
            else
            {
               // MyLogger.GetInstance().Info("login failure");
                HttpContext.Session.Remove("username");
                return View("LoginFailure", user);
            }
        }
    }
}
