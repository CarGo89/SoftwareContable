using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using AutoMapper;
using Dpr.Core.DataAccess;
using SoftwareContable.DataAccess;
using SoftwareContable.DataAccess.Entities;
using SoftwareContable.Extensions;
using SoftwareContable.Models;
using SoftwareContable.Utilities;

namespace SoftwareContable.Controllers
{
    public abstract class SoftwareContableController<TModel, TData> : Controller
        where TModel : class, IModel
        where TData : class, IEntity
    {
        #region Fields

        protected readonly IRepository<TData> ModelRepository;

        protected readonly string ModelDescription;

        protected static readonly IDictionary<Type, string> ModelDescriptionsByType;

        protected static readonly SettingsManager Settings;

        protected string SiteBaseUrl
        {
            get
            {
                if (Request != null && Request.Url != null)
                {
                    return string.Format("{0}://{1}", Request.Url.Scheme, Request.Url.Authority);
                }

                return null;
            }
        }

        #endregion Fields

        #region Constructors

        static SoftwareContableController()
        {
            ModelDescriptionsByType = new Dictionary<Type, string>();

            Settings = SettingsManager.Instance;
        }

        protected SoftwareContableController()
        {
            var tModel = typeof(TModel);

            ModelRepository = FactoryRepository.Create<SoftwareContableDbContext, TData>();

            if (!ModelDescriptionsByType.TryGetValue(tModel, out ModelDescription))
            {
                var descriptionAttr = tModel.GetCustomAttributes(true).OfType<DisplayNameAttribute>().SingleOrDefault();

                ModelDescription = descriptionAttr == null ? "modelElements" : descriptionAttr.DisplayName;

                ModelDescriptionsByType[tModel] = ModelDescription;
            }
        }

        #endregion Constructors

        #region Action Results

        [HttpGet]
        public virtual async Task<ActionResult> Get()
        {
            var dbModels = await ModelRepository.GetAllAsync();
            var models = Mapper.Map<IEnumerable<TModel>>(dbModels);

            var jsonResult = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase)
            {
                { ModelDescription, models }
            };

            return jsonResult.ToJsonResult();
        }

        [HttpPost]
        public virtual async Task<ActionResult> Save(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var dbModel = Mapper.Map<TData>(model);
            var savedDbModel = await ModelRepository.AddAsync(dbModel);

            model = Mapper.Map<TModel>(savedDbModel);

            return model.ToJsonResult();
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var dbModel = Mapper.Map<TData>(model);

            dbModel = await ModelRepository.UpdateAsync(dbModel);

            model = Mapper.Map<TModel>(dbModel);

            return model.ToJsonResult();
        }

        [HttpPost]
        public virtual ActionResult Validate(TModel model)
        {
            if (ModelState.IsValid)
            {
                return true.ToJsonResult();
            }

            return GetValidationMessages().ToJsonResult();
        }

        #endregion  Action Results

        #region Methods

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                LoggedInUserInfo.PrincipalUser = filterContext.HttpContext.User;
            }

            base.OnAuthentication(filterContext);
        }

        protected IEnumerable<string> GetValidationMessages()
        {
            return ModelState
               .Where(model => model.Value.Errors.Any())
               .SelectMany(model => model.Value.Errors.Select(error => error.ErrorMessage));
        }

        protected override void Dispose(bool disposing)
        {
            ModelRepository.Dispose();

            base.Dispose(disposing);
        }

        #endregion Methods
    }
}