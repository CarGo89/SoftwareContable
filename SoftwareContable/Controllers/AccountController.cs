using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (!XsltParameters.ContainsKey(XsltUsersUrlParam))
            {
                XsltParameters[XsltUsersUrlParam] = string.Concat(SiteBaseUrl, Url.Action("Index", "User"));
            }

            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var existsUserName = await ModelRepository
                .Single(existingUser => string.Equals(existingUser.UserName, user.UserName, StringComparison.InvariantCultureIgnoreCase)).ConfigureAwait(false);

            if (existsUserName != null)
            {
                return string.Format("El usuario '{0}' ya existe en el sistema", user.UserName).ToJsonResult();
            }

            user.RoleId = Settings.DefaultUserRoleDbId;
            user.IsAuthorized = false;

            var userToSave = Mapper.Map<DataAccess.Entities.User>(user);

            userToSave = await ModelRepository.Add(userToSave).ConfigureAwait(false);

            var savedUser = Mapper.Map<User>(userToSave);

            var recipients = (await ModelRepository
                .GetAll(admin => admin.RoleId == Settings.AdministratorUserRoleDbId))
                .Select(admin => admin.Email);

            var htmlBody = user.GetHtmlFromXslt(Server.MapPath(Settings.NewUserTemplateFilePath), XsltParameters);

            await _emailSender.Send(Settings.SmtpEmail, recipients, Settings.NewUserAuthorizationSubject, htmlBody);

            return savedUser.ToJsonResult();
        }

        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            return RedirectToAction("Login");
        }
    }
}