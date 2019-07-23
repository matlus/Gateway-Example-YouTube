using MovieService.Soap;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace MovieService
{
    public static class WcfRouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Add(new ServiceRoute("Movies", new ServiceHostFactory(), typeof(MoviesContract)));
        }
    }
}