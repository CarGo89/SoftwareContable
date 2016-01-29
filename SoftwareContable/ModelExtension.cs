using System.Web.Mvc;

namespace SoftwareContable
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
    }
}