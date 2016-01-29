using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.Models;

namespace SoftwareContable.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRepository<DataAccess.Entities.Report> _reportRepository;

        public ReportController()
        {
            _reportRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.Report>();
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
        public async Task<ActionResult> Get()
        {
            var dbReports = await _reportRepository.GetAll();
            var reports = Mapper.Map<IEnumerable<Report>>(dbReports);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "reports", reports }
            };

            return jsonResult.ToJsonResult();
        }

        protected override void Dispose(bool disposing)
        {
            _reportRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}