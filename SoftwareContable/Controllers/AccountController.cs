using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using SoftwareContable.Models;
using SoftwareContable.Utilities;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class AccountController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        private readonly IEmailSender _emailSender;

        public AccountController()
        {
            _emailSender = new EmailSender(Settings.SmtpServer);
        }

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
            user.RoleId = Settings.DefaultUserRoleDbId;
            user.IsAuthorized = false;

            var savedUser = await Save(user);
            var recipients = (await ModelRepository
                .GetAll(admin => admin.RoleId == Settings.AdministratorUserRoleDbId))
                .Select(admin => admin.Email);

            var usersUrl = string.Concat(SiteBaseUrl, Url.Action("Index", "User"));
            var parameters = new Dictionary<string, object> { { "UsersUrlParam", usersUrl } };
            var emailBody = user.GetHtmlFromXslt(Server.MapPath(Settings.NewUserTemplateFilePath), parameters);

            await _emailSender.Send(Settings.SmtpEmail, recipients, Settings.NewUserAuthorizationSubject, emailBody);

            return savedUser;
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}