using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SoftwareContable.Models;

namespace SoftwareContable.Extensions
{
    public static class ModelExtension
    {
        public static JsonResult ToJsonResult(this object target)
        {
            return new JsonResult
            {
                Data = target,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
        }

        public static IDictionary<string, OptionCatalog> ToJson(this IEnumerable<OptionCatalog> elements)
        {
            return elements.ToDictionary(key => key.Id.ToString(), StringComparer.InvariantCultureIgnoreCase);
        }
    }
}