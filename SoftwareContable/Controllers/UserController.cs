using System.Web.Mvc;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    public class UserController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}