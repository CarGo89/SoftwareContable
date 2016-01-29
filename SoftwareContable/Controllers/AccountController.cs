using System.Threading.Tasks;
using System.Web.Mvc;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class AccountController : SoftwareContableController<User, DataAccess.Entities.User>
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

        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(User user)
        {
            user.RoleId = 2;

            var savedUser = await Save(user);

            return savedUser;
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}