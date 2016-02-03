using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
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

        public static string Serialize<TModel>(this TModel model)
            where TModel : IModel
        {
            var xmlSerializer = new XmlSerializer(typeof(TModel));

            using (var stringWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(stringWriter))
                {
                    xmlSerializer.Serialize(xmlWriter, model);

                    return stringWriter.ToString();
                }
            }
        }
    }
}