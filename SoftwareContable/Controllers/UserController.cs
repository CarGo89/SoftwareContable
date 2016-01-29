using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<DataAccess.Entities.User> _userRepository;

        public UserController()
        {
            _userRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.User>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var dbUsers = await _userRepository.GetAll();
            var users = Mapper.Map<IEnumerable<User>>(dbUsers);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "users", users }
            };

            return jsonResult.ToJsonResult();
        }

        protected override void Dispose(bool disposing)
        {
            _userRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}