using System.Web.Mvc;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class HomeController : SoftwareContableController<User, DataAccess.Entities.User>
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}