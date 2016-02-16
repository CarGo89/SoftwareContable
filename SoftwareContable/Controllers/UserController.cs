using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using SoftwareContable.Extensions;
using SoftwareContable.Models;
using SoftwareContable.Utilities;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class UserController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public override async Task<ActionResult> Get()
        {
            var dbUsers = await ModelRepository.GetAllAsync();
            var users = Mapper.Map<IEnumerable<User>>(dbUsers);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "users", users },
                { "isLoggedInUserAdmin", LoggedInUserInfo.User.IsAdministrator() }
            };

            return jsonResult.ToJsonResult();
        }

        [HttpPut]
        public async Task<ActionResult> Authorize(int userId)
        {
            if (!LoggedInUserInfo.User.IsAdministrator())
            {
                return "Sólo administradores pueden autorizar acceso a usuarios.".ToJsonResult();
            }

            var userToAuthorize = await ModelRepository
                .SingleAsync(user => user.Id == userId).ConfigureAwait(false);

            if (userToAuthorize == null)
            {
                return "El usuario no existe.".ToJsonResult();
            }

            if (userToAuthorize.IsAuthorized)
            {
                return userToAuthorize.ToJsonResult();
            }

            userToAuthorize.IsAuthorized = true;
            userToAuthorize.AuthorizedByUserId = LoggedInUserInfo.User.Id;
            userToAuthorize.AuthorizationDate = DateTime.Now;

            await ModelRepository.UpdateAsync(userToAuthorize).ConfigureAwait(false);

            return true.ToJsonResult();
        }
    }
}