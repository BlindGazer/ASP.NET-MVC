using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using AutoMapper;
using FastBus.Services;

namespace FastBus.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MigrationConfig.RegisterMigrator();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<WebMappingProfile>();
                cfg.AddProfile<ServiceMappingProfile>();
            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}