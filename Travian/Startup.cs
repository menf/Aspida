using Microsoft.Owin.Cors;
using Owin;
using System.Web.Http;
using Hangfire;
using Hangfire.MemoryStorage;
namespace Travian
{

    public class Startup 
    { 
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder) 
        { 
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute( 
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional } 
            );
            appBuilder.UseCors(CorsOptions.AllowAll);
            GlobalConfiguration.Configuration
               .UseMemoryStorage();

            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();

            appBuilder.UseWebApi(config); 
        } 
    } 
}
