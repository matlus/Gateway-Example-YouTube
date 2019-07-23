using Movies.DomainLayer;
using Newtonsoft.Json;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MovieService
{
    public class WebApiApplication : HttpApplication
    {
        public DomainFacade DomainFacade { get; private set; }
        public override void Init()
        {
            base.Init();
            DomainFacade = new DomainFacade();

            var jsonSerializerSettings = new Newtonsoft.Json.JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            // Need to do this so we can use the JsonConvert in our own code
            JsonConvert.DefaultSettings = (() =>
            {
                return jsonSerializerSettings;
            });

            // Need to do this so the WebAPI toolkit uses the same settings
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jsonSerializerSettings;
        }

        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            WcfRouteConfig.RegisterRoutes(RouteTable.Routes);
            
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
