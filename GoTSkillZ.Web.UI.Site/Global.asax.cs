using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace GoTSkillZ.Web.UI.Site
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

      
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }
        }
    }
}