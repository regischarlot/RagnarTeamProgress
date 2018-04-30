using System.Web.Mvc;
using System.Web.Routing;
 
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TeamProgress.ExtNetConfig), "Start")]

namespace TeamProgress 
{
    public static class ExtNetConfig 
    {
        public static void Start() 
        {
            ExtNetConfig.RegisterRoutes(RouteTable.Routes);
        }
 
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Ignore all ext.axd embedded resource paths
            routes.IgnoreRoute("{extnet-root}/{extnet-file}/ext.axd");

            // Add http://example.com/extnet/ Route
            routes.MapRoute(
                "ExtNet", // Route name
                "extnet/{action}/{id}", // URL with parameters
                new { controller = "ExtNet", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }
    }
}