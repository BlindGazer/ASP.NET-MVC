namespace FastBus.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        GovermentNumber = c.String(nullable: false, maxLength: 20),
                        Color = c.String(nullable: false, maxLength: 20),
                        Seats = c.Int(nullable: false),
                        Year = c.Short(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomRoutes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ReturnDate = c.DateTime(nullable: false),
                        Distane = c.Short(),
                        Other = c.String(),
                        CustomerId = c.Int(nullable: false),
                        Departure = c.String(),
                        Destination = c.String(),
                        DepartureDate = c.DateTime(),
                        DestinationDate = c.DateTime(),
                        CarId = c.Int(nullable: false),
                        CreaterId = c.Int(nullable: false),
                        Cost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreaterId)
                .ForeignKey("dbo.Users", t => t.CustomerId)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CustomerId)
                .Index(t => t.CarId)
                .Index(t => t.CreaterId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RegistredDate = c.DateTime(nullable: false, storeType: "date"),
                        DateBorn = c.DateTime(storeType: "date"),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Departure = c.String(nullable: false, maxLength: 100),
                        Destination = c.String(nullable: false, maxLength: 100),
                        DepartureDate = c.DateTime(),
                        DestinationDate = c.DateTime(),
                        CarId = c.Int(nullable: false),
                        CreaterId = c.Int(nullable: false),
                        Cost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreaterId)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.CarId)
                .Index(t => t.CreaterId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RouteId = c.Long(nullable: false),
                        IsReserve = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.RouteWayPoints",
                c => new
                    {
                        RouteId = c.Long(nullable: false),
                        WayPointId = c.Int(nullable: false),
                        Sequence = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteId, t.WayPointId })
                .ForeignKey("dbo.WayPoints", t => t.WayPointId)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .Index(t => t.RouteId)
                .Index(t => t.WayPointId);
            
            CreateTable(
                "dbo.WayPoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 500),
                        UserId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.DriversRequiringApproval",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 75),
                        DateBorn = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Byte(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Republic = c.String(nullable: false, maxLength: 25),
                        Decription = c.String(nullable: false, maxLength: 500),
                        Address = c.String(nullable: false, maxLength: 200),
                        Phones = c.String(nullable: false, maxLength: 200),
                        Emails = c.String(nullable: false, maxLength: 200),
                        PostCode = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HistoryLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        LogDate = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertieLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PropertyName = c.String(),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        LogId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HistoryLogs", t => t.LogId)
                .Index(t => t.LogId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriversCars",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CarId })
                .ForeignKey("dbo.Users", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.DriversCustomRoutes",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CustomRouteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CustomRouteId })
                .ForeignKey("dbo.Users", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.CustomRoutes", t => t.CustomRouteId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CustomRouteId);
            
            CreateTable(
                "dbo.DriversRoutes",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        RouteId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.RouteId })
                .ForeignKey("dbo.Users", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.DriversRequiringApprovalCars",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CarId })
                .ForeignKey("dbo.DriversRequiringApproval", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CarId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PropertieLogs", "LogId", "dbo.HistoryLogs");
            DropForeignKey("dbo.Routes", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DriversRequiringApprovalCars", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DriversRequiringApprovalCars", "DriverId", "dbo.DriversRequiringApproval");
            DropForeignKey("dbo.CustomRoutes", "CarId", "dbo.Cars");
            DropForeignKey("dbo.CustomRoutes", "CustomerId", "dbo.Users");
            DropForeignKey("dbo.CustomRoutes", "CreaterId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.DriversRoutes", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.DriversRoutes", "DriverId", "dbo.Users");
            DropForeignKey("dbo.DriversCustomRoutes", "CustomRouteId", "dbo.CustomRoutes");
            DropForeignKey("dbo.DriversCustomRoutes", "DriverId", "dbo.Users");
            DropForeignKey("dbo.RouteWayPoints", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteWayPoints", "WayPointId", "dbo.WayPoints");
            DropForeignKey("dbo.Tickets", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "CreaterId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.DriversCars", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DriversCars", "DriverId", "dbo.Users");
            DropIndex("dbo.DriversRequiringApprovalCars", new[] { "CarId" });
            DropIndex("dbo.DriversRequiringApprovalCars", new[] { "DriverId" });
            DropIndex("dbo.DriversRoutes", new[] { "RouteId" });
            DropIndex("dbo.DriversRoutes", new[] { "DriverId" });
            DropIndex("dbo.DriversCustomRoutes", new[] { "CustomRouteId" });
            DropIndex("dbo.DriversCustomRoutes", new[] { "DriverId" });
            DropIndex("dbo.DriversCars", new[] { "CarId" });
            DropIndex("dbo.DriversCars", new[] { "DriverId" });
            DropIndex("dbo.PropertieLogs", new[] { "LogId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.RouteWayPoints", new[] { "WayPointId" });
            DropIndex("dbo.RouteWayPoints", new[] { "RouteId" });
            DropIndex("dbo.Tickets", new[] { "RouteId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            DropIndex("dbo.Routes", new[] { "CreaterId" });
            DropIndex("dbo.Routes", new[] { "CarId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.CustomRoutes", new[] { "CreaterId" });
            DropIndex("dbo.CustomRoutes", new[] { "CarId" });
            DropIndex("dbo.CustomRoutes", new[] { "CustomerId" });
            DropTable("dbo.DriversRequiringApprovalCars");
            DropTable("dbo.DriversRoutes");
            DropTable("dbo.DriversCustomRoutes");
            DropTable("dbo.DriversCars");
            DropTable("dbo.Roles");
            DropTable("dbo.PropertieLogs");
            DropTable("dbo.HistoryLogs");
            DropTable("dbo.Companies");
            DropTable("dbo.DriversRequiringApproval");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.UserLogins");
            DropTable("dbo.WayPoints");
            DropTable("dbo.RouteWayPoints");
            DropTable("dbo.Tickets");
            DropTable("dbo.Routes");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.CustomRoutes");
            DropTable("dbo.Cars");
        }
    }
}
