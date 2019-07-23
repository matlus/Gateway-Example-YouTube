using Movies.DomainLayer;
using Movies.DomainLayer.Managers.DomainModels;
using Movies.DomainLayer.Managers.Enums;
using Movies.DomainLayer.Managers.Exceptions;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace MovieService.Soap
{
    [ServiceContract(Namespace="http://www.matlus.com/gateway/Movies")]
    [ServiceBehavior(Namespace="http://www.matlus.com/gateway/Movies", InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MoviesContract
    {
        private DomainFacade DomainFacade
        {
            get
            {
                // IMPORTANT: Don't use backing field for this. 
                return this.MakeDomainFacade();
            }
        }

        protected virtual DomainFacade MakeDomainFacade()
        {
            return ((WebApiApplication)HttpContext.Current.ApplicationInstance).DomainFacade;
        }


        [OperationContract]
        public IEnumerable<Movie> GetMoviesByGenre(Genre genre)
        {
            try
            {
                return DomainFacade.GetMoviesByGenre(genre);
            }
            catch (Exception e)
            {
                throw ConstructSoapFaultException(e);                
            }            
        }

        [OperationContract]
        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return DomainFacade.GetMoviesByYear(year);
        }

        private FaultException ConstructSoapFaultException(Exception e)
        {
            var moviesException = e as MoviesBaseException;

            string faultReason;

            if (moviesException != null)
            {
                faultReason = moviesException.Reason + "\r\n" + e.Message;
            }
            else
            {
                faultReason = e.Message;
            }

            return new FaultException(faultReason, FaultCode.CreateSenderFaultCode(e.GetType().Name, "http://www.matlus.com/gateway/Movies"));
        }
    }
}