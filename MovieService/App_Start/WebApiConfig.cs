using System.Web.Http;

namespace MovieService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)        
        {
            config.MapHttpAttributeRoutes();
        }
    }
}
