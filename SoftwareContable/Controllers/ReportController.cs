using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.Extensions;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class ReportController : SoftwareContableController<Report, DataAccess.Entities.Report>
    {
        private readonly IRepository<DataAccess.Entities.SoldSystem> _soldSystemRepository;
        private readonly IRepository<DataAccess.Entities.Client> _clientRepository;

        public ReportController()
        {
            _soldSystemRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.SoldSystem>();

            _clientRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.Client>();
        }

        public ActionResult Visualize()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Catalogs()
        {
            var dbSoldSystems = await _soldSystemRepository.GetAllAsync();
            var soldSystems = Mapper.Map<IEnumerable<OptionCatalog>>(dbSoldSystems);
            var dbClients = await _clientRepository.GetAllAsync();
            var clients = Mapper.Map<IEnumerable<Client>>(dbClients);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "clients", clients },
                { "soldSystems", soldSystems }
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