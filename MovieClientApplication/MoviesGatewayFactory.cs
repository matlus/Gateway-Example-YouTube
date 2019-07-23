using MovieClientApplication.Gateways;
using System.Collections.Generic;

namespace MovieClientApplication
{
    internal static class MoviesGatewayFactory
    {
        private static Dictionary<string, MoviesGatewayBase> moviesGateways = new Dictionary<string, MoviesGatewayBase>();
        public static MoviesGatewayBase CreateGateway(string protocol, string serviceBaseAddress)
        {
            MoviesGatewayBase moviesGateway = null;

            if (moviesGateways.TryGetValue(protocol, out moviesGateway))
            {
                return moviesGateway;
            }


            if (protocol == "REST")
            {
                moviesGateway = new MoviesGatewayRest(serviceBaseAddress);
            }
            else if (protocol == "SOAP")
            {
                moviesGateway = new MoviesGatewaySoap(serviceBaseAddress);
            }

            moviesGateways.Add(protocol, moviesGateway);
            return moviesGateway;
        }
    }
}
