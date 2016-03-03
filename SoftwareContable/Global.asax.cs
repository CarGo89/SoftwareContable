using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using AutoMapper;
using SoftwareContable.DataAccess;
using SoftwareContable.DataAccess.Entities;
using SoftwareContable.Mappers;
using SoftwareContable.Utilities;

namespace SoftwareContable
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(mapper => mapper.AddProfile<ModelMapperProfile>());

            using (var dbContext = new SoftwareContableDbContext())
            {
                dbContext.Database.CreateIfNotExists();

                dbContext.Database.Initialize(true);

                if (!dbContext.Roles.Any())
                {
                    dbContext.Roles.AddRange(new[]
                    {
                        new Role { Name = "Administrador" },
                        new Role { Name = "Usuario" }
                    });

                    dbContext.SaveChanges();
                }

                if (!dbContext.SoldSystems.Any())
                {
                    dbContext.SoldSystems.AddRange(new[]
                    {
                        new SoldSystem { Name = "Contabilidad" },
                        new SoldSystem { Name = "AdminPAQ" },
                        new SoldSystem { Name = "ContPAQ" },
                        new SoldSystem { Name = "Bancos" },
                        new SoldSystem { Name = "Facturación Electrónica" },
                        new SoldSystem { Name = "Nóminas" }
                    });

                    dbContext.SaveChanges();
                }

                if (!dbContext.Users.Any())
                {
                    dbContext.Users.Add(
                        new User
                        {
                            UserName = "uno",
                            Password = "123456",
                            Email = "administrador@desarrollosoftwarecontable.com",
                            RoleId = SettingsManager.Instance.AdministratorUserRoleDbId,
                            AuthorizationDate = DateTime.Now,
                            IsAuthorized = true
                        });

                    dbContext.SaveChanges();
                }
            }
        }
    }
}