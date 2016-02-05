using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using SoftwareContable.Extensions;
using SoftwareContable.Models;
using SoftwareContable.Utilities;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class AccountController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        private const string XsltUsersUrlParam = "UsersUrlParam";

        private static readonly Dictionary<string, object> XsltParameters;

        private readonly IEmailSender _emailSender;

        static AccountController()
        {
            XsltParameters = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        public AccountController()
        {
            _emailSender = new EmailSender(Settings.SmtpServer);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(User user, string returnUrl = "")
        {
            var existingUser = await ModelRepository.SingleAsync(dbUser => dbUser.IsAuthorized &&
                string.Equals(dbUser.UserName, user.UserName, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(dbUser.Password, user.Password));

            if (existingUser == null)
            {
                return "El usuario o la contraseña es incorrecta.".ToJsonResult();
            }

            FormsAuthentication.SetAuthCookie(user.UserName, true);

            returnUrl = string.IsNullOrWhiteSpace(returnUrl) ? Url.Action("Index", "Home") : returnUrl;

            return Json(new { returnUrl });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (!XsltParameters.ContainsKey(XsltUsersUrlParam))
            {
                XsltParameters[XsltUsersUrlParam] = string.Concat(SiteBaseUrl, Url.Action("Index", "User"));
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var existsUserName = await ModelRepository
                .SingleAsync(existingUser => string.Equals(existingUser.UserName, user.UserName, StringComparison.InvariantCultureIgnoreCase)).ConfigureAwait(false);

            if (existsUserName != null)
            {
                return string.Format("El usuario '{0}' ya existe en el sistema", user.UserName).ToJsonResult();
            }

            user.RoleId = Settings.DefaultUserRoleDbId;
            user.IsAuthorized = false;

            var userToSave = Mapper.Map<DataAccess.Entities.User>(user);

            userToSave = await ModelRepository.AddAsync(userToSave).ConfigureAwait(false);

            var savedUser = Mapper.Map<User>(userToSave);

            var recipients = (await ModelRepository
                .GetAllAsync(admin => admin.RoleId == Settings.AdministratorUserRoleDbId))
                .Select(admin => admin.Email);

            var htmlBody = user.GetHtmlFromXslt(Server.MapPath(Settings.NewUserTemplateFilePath), XsltParameters);

            await _emailSender.Send(Settings.SmtpEmail, recipients, Settings.NewUserAuthorizationSubject, htmlBody);

            return savedUser.ToJsonResult();
        }

        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}