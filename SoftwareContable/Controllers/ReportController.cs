using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.Extensions;
using SoftwareContable.Models;
using SoftwareContable.Utilities;

namespace SoftwareContable.Controllers
{
    [Authorize]
    public class ReportController : SoftwareContableController<Report, DataAccess.Entities.Report>
    {
        private const string XsltReportUrlParam = "ReportUrlParam";

        private const string XsltReportFolioUrlParam = "ReportFolioParam";

        private static readonly Dictionary<string, object> XsltParameters;

        private readonly IRepository<DataAccess.Entities.SoldSystem> _soldSystemRepository;

        private readonly IRepository<DataAccess.Entities.Client> _clientRepository;

        private readonly IRepository<DataAccess.Entities.User> _userRepository;

        private readonly IEmailSender _emailSender;

        static ReportController()
        {
            XsltParameters = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
        }

        public ReportController()
        {
            _soldSystemRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.SoldSystem>();

            _clientRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.Client>();

            _userRepository = FactoryRepository.Create<DataAccess.SoftwareContableDbContext, DataAccess.Entities.User>();

            _emailSender = new EmailSender(Settings.SmtpServer);
        }

        public ActionResult Visualize()
        {
            return View();
        }

        public ActionResult Create()
        {
            if (!XsltParameters.ContainsKey(XsltReportUrlParam))
            {
                XsltParameters[XsltReportUrlParam] = string.Concat(SiteBaseUrl, Url.Action("Visualize", "Report"));
            }

            return View();
        }

        [HttpPost]
        public override async Task<ActionResult> Save(Report report)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var dbReport = Mapper.Map<DataAccess.Entities.Report>(report);
            var savedReport = await ModelRepository.AddAsync(dbReport);

            report = Mapper.Map<Report>(savedReport);

            var recipients = (await _userRepository
                .GetAllAsync(admin => admin.RoleId == Settings.AdministratorUserRoleDbId))
                .Select(admin => admin.Email);

            XsltParameters[XsltReportFolioUrlParam] = report.Id;

            var htmlBody = LoggedInUserInfo.User.GetHtmlFromXslt(Server.MapPath(Settings.NewReportTemplateFilePath), XsltParameters);
            var subject = string.Format(Settings.NewReportSubject, report.Id);

            await _emailSender.Send(Settings.SmtpEmail, recipients, subject, htmlBody);

            return report.ToJsonResult();
        }

        [HttpGet]
        public async Task<ActionResult> Catalogs()
        {
            var dbSoldSystems = await _soldSystemRepository.GetAllAsync();
            var soldSystems = Mapper.Map<IEnumerable<OptionCatalog>>(dbSoldSystems);
            var dbClients = await _clientRepository.GetAllAsync();
            var clients = Mapper.Map<IEnumerable<Client>>(dbClients);
            var jsonClients = clients
                .ToDictionary(key => key.Id.ToString(), StringComparer.InvariantCultureIgnoreCase);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { "clients", clients },
                { "clientsById", jsonClients },
                { "soldSystems", soldSystems }
            };

            return jsonResult.ToJsonResult();
        }

        [HttpGet]
        public virtual async Task<ActionResult> Search(int initialFolio, int finalFolio)
        {
            var dbReports = await ModelRepository
                .GetAllAsync(report => report.Id >= initialFolio && report.Id <= finalFolio);
            var reports = Mapper.Map<IEnumerable<Report>>(dbReports);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { ModelDescription, reports }
            };

            return jsonResult.ToJsonResult();
        }

        protected override void Dispose(bool disposing)
        {
            _soldSystemRepository.Dispose();

            _clientRepository.Dispose();

            _soldSystemRepository.Dispose();

            base.Dispose(disposing);
        }
    }
}