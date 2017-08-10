namespace FastBus.Persistence.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 25),
                        LastName = c.String(nullable: false, maxLength: 25),
                        Patronymic = c.String(maxLength: 25),
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
                        IsCanReserve = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
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
                "dbo.CustomRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Departure = c.String(),
                        Destination = c.String(),
                        BuyerId = c.Int(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        Distane = c.Short(),
                        Comment = c.String(),
                        DepartureDate = c.DateTime(nullable: false),
                        DestinationDate = c.DateTime(nullable: false),
                        CarId = c.Int(nullable: false),
                        DispatcherId = c.Int(nullable: false),
                        Cost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .ForeignKey("dbo.Users", t => t.DispatcherId)
                .ForeignKey("dbo.Users", t => t.BuyerId)
                .Index(t => t.BuyerId)
                .Index(t => t.CarId)
                .Index(t => t.DispatcherId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 50),
                        GovermentNumber = c.String(nullable: false, maxLength: 20),
                        Color = c.String(nullable: false, maxLength: 20),
                        Seats = c.Byte(nullable: false),
                        GarageNumber = c.Int(),
                        Year = c.Short(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ScheduleItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RouteId = c.Int(nullable: false),
                        Seats = c.Byte(nullable: false),
                        Number = c.Int(nullable: false),
                        DepartureDate = c.DateTime(nullable: false),
                        DestinationDate = c.DateTime(nullable: false),
                        CarId = c.Int(nullable: false),
                        DispatcherId = c.Int(nullable: false),
                        Cost = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.DispatcherId)
                .ForeignKey("dbo.Routes", t => t.RouteId)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .Index(t => t.RouteId)
                .Index(t => t.CarId)
                .Index(t => t.DispatcherId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Departure = c.String(nullable: false, maxLength: 100),
                        Destination = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RouteWayPoints",
                c => new
                    {
                        RouteId = c.Int(nullable: false),
                        WayPointId = c.Int(nullable: false),
                        Order = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.RouteId, t.WayPointId })
                .ForeignKey("dbo.WayPoints", t => t.WayPointId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
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
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BuyerId = c.Int(nullable: false),
                        ScheduleId = c.Long(nullable: false),
                        IsReserve = c.Boolean(nullable: false),
                        IsPaid = c.Boolean(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScheduleItems", t => t.ScheduleId)
                .ForeignKey("dbo.Users", t => t.BuyerId)
                .Index(t => t.BuyerId)
                .Index(t => t.ScheduleId);
            
            CreateTable(
                "dbo.DriversRequiringApproval",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 75),
                        DateBorn = c.DateTime(storeType: "date"),
                        Driver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.Users", t => t.Driver_Id)
                .Index(t => t.Driver_Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(nullable: false, maxLength: 500),
                        BuyerId = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.BuyerId)
                .Index(t => t.BuyerId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Byte(nullable: false),
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
                        Create = c.DateTime(nullable: false),
                        UserId = c.Int(),
                        BuyerId = c.Int(),
                        DispatcherId = c.Int(),
                        DriverId = c.Int(),
                        RouteId = c.Int(),
                        ScheduleItemId = c.Long(),
                        CarId = c.Int(),
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
                "dbo.DriversSchedule",
                c => new
                    {
                        DriverId = c.Long(nullable: false),
                        ScheduleItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.ScheduleItemId })
                .ForeignKey("dbo.ScheduleItems", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ScheduleItemId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.ScheduleItemId);
            
            CreateTable(
                "dbo.DriversCars",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CarId })
                .ForeignKey("dbo.Cars", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CarId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CarId);
            
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
            
            CreateTable(
                "dbo.DriversCustomRoutes",
                c => new
                    {
                        DriverId = c.Int(nullable: false),
                        CustomRouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DriverId, t.CustomRouteId })
                .ForeignKey("dbo.CustomRoutes", t => t.DriverId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CustomRouteId, cascadeDelete: true)
                .Index(t => t.DriverId)
                .Index(t => t.CustomRouteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.PropertieLogs", "LogId", "dbo.HistoryLogs");
            DropForeignKey("dbo.Tickets", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.CustomRoutes", "BuyerId", "dbo.Users");
            DropForeignKey("dbo.DriversCustomRoutes", "CustomRouteId", "dbo.Users");
            DropForeignKey("dbo.DriversCustomRoutes", "DriverId", "dbo.CustomRoutes");
            DropForeignKey("dbo.ScheduleItems", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DriversRequiringApproval", "Driver_Id", "dbo.Users");
            DropForeignKey("dbo.DriversRequiringApprovalCars", "CarId", "dbo.Cars");
            DropForeignKey("dbo.DriversRequiringApprovalCars", "DriverId", "dbo.DriversRequiringApproval");
            DropForeignKey("dbo.DriversCars", "CarId", "dbo.Users");
            DropForeignKey("dbo.DriversCars", "DriverId", "dbo.Cars");
            DropForeignKey("dbo.Tickets", "ScheduleId", "dbo.ScheduleItems");
            DropForeignKey("dbo.RouteWayPoints", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.RouteWayPoints", "WayPointId", "dbo.WayPoints");
            DropForeignKey("dbo.ScheduleItems", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.DriversSchedule", "ScheduleItemId", "dbo.Users");
            DropForeignKey("dbo.DriversSchedule", "DriverId", "dbo.ScheduleItems");
            DropForeignKey("dbo.ScheduleItems", "DispatcherId", "dbo.Users");
            DropForeignKey("dbo.CustomRoutes", "DispatcherId", "dbo.Users");
            DropForeignKey("dbo.CustomRoutes", "CarId", "dbo.Cars");
            DropIndex("dbo.DriversCustomRoutes", new[] { "CustomRouteId" });
            DropIndex("dbo.DriversCustomRoutes", new[] { "DriverId" });
            DropIndex("dbo.DriversRequiringApprovalCars", new[] { "CarId" });
            DropIndex("dbo.DriversRequiringApprovalCars", new[] { "DriverId" });
            DropIndex("dbo.DriversCars", new[] { "CarId" });
            DropIndex("dbo.DriversCars", new[] { "DriverId" });
            DropIndex("dbo.DriversSchedule", new[] { "ScheduleItemId" });
            DropIndex("dbo.DriversSchedule", new[] { "DriverId" });
            DropIndex("dbo.PropertieLogs", new[] { "LogId" });
            DropIndex("dbo.Reviews", new[] { "BuyerId" });
            DropIndex("dbo.DriversRequiringApproval", new[] { "Driver_Id" });
            DropIndex("dbo.Tickets", new[] { "ScheduleId" });
            DropIndex("dbo.Tickets", new[] { "BuyerId" });
            DropIndex("dbo.RouteWayPoints", new[] { "WayPointId" });
            DropIndex("dbo.RouteWayPoints", new[] { "RouteId" });
            DropIndex("dbo.ScheduleItems", new[] { "DispatcherId" });
            DropIndex("dbo.ScheduleItems", new[] { "CarId" });
            DropIndex("dbo.ScheduleItems", new[] { "RouteId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.CustomRoutes", new[] { "DispatcherId" });
            DropIndex("dbo.CustomRoutes", new[] { "CarId" });
            DropIndex("dbo.CustomRoutes", new[] { "BuyerId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropTable("dbo.DriversCustomRoutes");
            DropTable("dbo.DriversRequiringApprovalCars");
            DropTable("dbo.DriversCars");
            DropTable("dbo.DriversSchedule");
            DropTable("dbo.Roles");
            DropTable("dbo.PropertieLogs");
            DropTable("dbo.HistoryLogs");
            DropTable("dbo.Companies");
            DropTable("dbo.Reviews");
            DropTable("dbo.DriversRequiringApproval");
            DropTable("dbo.Tickets");
            DropTable("dbo.WayPoints");
            DropTable("dbo.RouteWayPoints");
            DropTable("dbo.Routes");
            DropTable("dbo.ScheduleItems");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Cars");
            DropTable("dbo.CustomRoutes");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
        }
    }
}
