using System.Data.Entity.Migrations;
using FastBus.DAL.Migrations;

namespace FastBus.Web
{
    public class MigrationConfig
    {
        public static void RegisterMigrator()
        {
            var migrator = new DbMigrator(new Configuration());
            migrator.Update();
        }
    }
}