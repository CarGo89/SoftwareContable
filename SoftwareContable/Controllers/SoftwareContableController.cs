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

        #endregion Fields

        #region Constructors

        static SoftwareContableController()
        {
            ModelDescriptionsByType = new Dictionary<Type, string>();
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
            var dbModels = await ModelRepository.GetAll();
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

            var dbApplication = Mapper.Map<TData>(model);
            var savedDbApplication = await ModelRepository.Add(dbApplication);

            model = Mapper.Map<TModel>(savedDbApplication);

            return model.ToJsonResult();
        }

        [HttpPut]
        public virtual async Task<ActionResult> Update(TModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetValidationMessages().ToJsonResult();
            }

            var dbApplication = Mapper.Map<TData>(model);

            dbApplication = await ModelRepository.Update(dbApplication);

            var updatedModel = Mapper.Map<TModel>(dbApplication);

            return updatedModel.ToJsonResult();
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
            //UserInfo.PrincipalUser = filterContext.HttpContext.User;

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