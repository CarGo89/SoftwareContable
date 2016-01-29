using System.Data.Entity;
using SoftwareContable.DataAccess.Entities;
using SoftwareContable.Migrations;

namespace SoftwareContable.DataAccess
{
    public class SoftwareContableDbContext : DbContext
    {
        public SoftwareContableDbContext()
            : base("name=SoftwareContable")
        {
            var migrationStrategy = new MigrateDatabaseToLatestVersion<SoftwareContableDbContext, Configuration>("SoftwareContable");

            Database.SetInitializer(migrationStrategy);
        }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<SoldSystem> SoldSystems { get; set; }

        public virtual DbSet<Report> Reports { get; set; }
    }
}