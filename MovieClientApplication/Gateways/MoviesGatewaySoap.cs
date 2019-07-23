using MovieClientApplication.Exceptions;
using MovieClientApplication.Models;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace MovieClientApplication.Gateways
{
    internal sealed class MoviesGatewaySoap : MoviesGatewayBase
    {
        private bool disposed = false;
        private readonly ChannelFactory<MoviesContract> moviesChannelFactory;
        private readonly MoviesContract moviesContractProxy;

        public MoviesGatewaySoap(string serviceBaseAddress)
        {
            moviesChannelFactory = new ChannelFactory<MoviesContract>(new BasicHttpBinding(), serviceBaseAddress + "Movies");
            moviesContractProxy = moviesChannelFactory.CreateChannel();

        }

        protected override IEnumerable<Movie> GetMoviesByGenreCore(Genre genre)
        {
            try
            {
                return moviesContractProxy.GetMoviesByGenre(genre);
            }
            catch (Exception e)
            {
                throw CreateDomainSpecificException(e);
            }
        }

        private Exception CreateDomainSpecificException(Exception e)
        {
            var faultException = e as FaultException;
            
            if (faultException != null)
            {
                switch (faultException.Code.Name)
                {
                    case "InvalidGenreException":
                        return new InvalidGenreException(faultException.Message);
                    default:
                        break;
                }
            }
            else if (e is EndpointNotFoundException)
            {
                // throw a technical exception here
                return e;
            }
            
            return e;
        }

        protected override IEnumerable<Movie> GetMoviesByYearCore(int year)
        {
            return moviesContractProxy.GetMoviesByYear(year);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (moviesContractProxy != null)
                    ((IDisposable)moviesContractProxy).Dispose();

                if (moviesChannelFactory != null)
                    moviesChannelFactory.Close();

                disposed = true;
            }
        }
    }

    [ServiceContract(Namespace = "http://www.matlus.com/gateway/Movies")]
    public interface MoviesContract
    {
        [OperationContract]
        IEnumerable<Movie> GetMoviesByGenre(Genre genre);

        [OperationContract]
        IEnumerable<Movie> GetMoviesByYear(int year);
    }
}
