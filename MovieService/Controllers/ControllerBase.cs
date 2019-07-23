using Movies.DomainLayer;
using Movies.DomainLayer.Managers.Exceptions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MovieService.Controllers
{
    public class ControllerBase : ApiController
    {
        protected DomainFacade DomainFacade
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

        protected HttpResponseException ConstructHttpResponseException(string requestData, Exception exception)
        {            
            Exception actualException = exception;
            while (actualException.InnerException != null)
            {
                actualException = actualException.InnerException;
            }
            
            
            Type actualExceptionType = actualException.GetType();
            HttpResponseMessage httpResponseMessage = null;

            var moviesBaseException = actualException as MoviesBaseException;

            if (moviesBaseException != null)
            {
                HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
                var notices = moviesBaseException.Notices;
                string reasonPhrase = moviesBaseException.Reason;

                if (actualExceptionType.IsSubclassOf(typeof(MoviesDomainException)))
                {
                    httpStatusCode = HttpStatusCode.BadRequest;
                }

                httpResponseMessage = new HttpResponseMessage
                {
                    Content = new StringContent(JsonConvert.SerializeObject(notices)),
                    StatusCode = httpStatusCode,
                    ReasonPhrase = reasonPhrase
                };
            }
            else
            {
                httpResponseMessage = new HttpResponseMessage
                {
                    Content = new StringContent("Request Data: " + requestData + "\r\n\r\nStack Trace: " + actualException.StackTrace),
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            httpResponseMessage.Headers.Add("Exception-Type", actualExceptionType.Name);
            return new HttpResponseException(httpResponseMessage);
        }
    }
}