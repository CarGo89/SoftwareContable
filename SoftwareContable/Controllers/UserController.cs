using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using SoftwareContable.Extensions;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    public class UserController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public override async Task<ActionResult> Get()
        {
            var dbUsers = await ModelRepository.GetAll();
            var users = Mapper.Map<IEnumerable<User>>(dbUsers);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "users", users },
                { "isLoggedInUserAdmin", LoggedInUser.IsAdministrator() }
            };

            return jsonResult.ToJsonResult();
        }

        [HttpPut]
        public async Task<ActionResult> Authorize(int userId)
        {
            if (!LoggedInUser.IsAdministrator())
            {
                return "Sólo administradores pueden autorizar acceso a usuarios.".ToJsonResult();
            }

            var userToAuthorize = await ModelRepository
                .Single(user => user.Id == userId).ConfigureAwait(false);

            if (userToAuthorize == null)
            {
                return "El usuario no existe.".ToJsonResult();
            }

            if (userToAuthorize.IsAuthorized)
            {
                return userToAuthorize.ToJsonResult();
            }

            userToAuthorize.IsAuthorized = true;
            userToAuthorize.AuthorizedByUserId = LoggedInUser.Id;

            await ModelRepository.Update(userToAuthorize).ConfigureAwait(false);

            return true.ToJsonResult();
        }
    }
}