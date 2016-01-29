using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using AutoMapper;
using SoftwareContable.Mappers;

namespace SoftwareContable
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(mapper => mapper.AddProfile<ModelMapperProfile>());
        }
    }
}