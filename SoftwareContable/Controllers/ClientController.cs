using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    public class ClientController : Controller
    {
        private readonly IRepository<DataAccess.Entities.Client> _clientRepository;

        public ClientController()
        {
            _clientRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.Client>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var dbClients = await _clientRepository.GetAll();
            var clients = Mapper.Map<IEnumerable<Client>>(dbClients);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "clients", clients }
            };

            return jsonResult.ToJsonResult();
        }

        protected override void Dispose(bool disposing)
        {
            _clientRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}