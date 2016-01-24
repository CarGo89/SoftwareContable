using System.Web.Mvc;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}