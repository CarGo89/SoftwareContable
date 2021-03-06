﻿using System.Web.Mvc;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class ClientController : SoftwareContableController<Client, DataAccess.Entities.Client>
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}