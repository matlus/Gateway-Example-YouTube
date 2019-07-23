using MovieClientApplication.Gateways.ExceptionTransformers;
using MovieClientApplication.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MovieClientApplication.Gateways
{
    internal sealed class MoviesGatewayRest : MoviesGatewayBase
    {
        private bool disposed = false;
        private readonly string jsonMediaType = "application/json";
        private readonly MediaTypeFormatter[] mediaTypeFormatters = new MediaTypeFormatter[] { new JsonMediaTypeFormatter() };
        private readonly string serviceBaseAddress;
        private readonly HttpClient httpClient;

        public MoviesGatewayRest(string serviceBaseAddress)
        {
            this.serviceBaseAddress = serviceBaseAddress;
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(serviceBaseAddress);
            httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(jsonMediaType));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("gzip"));
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(StringWithQualityHeaderValue.Parse("defalte"));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(new ProductHeaderValue("Matlus_HttpClient", "1.0")));

        }

        protected override IEnumerable<Movie> GetMoviesByGenreCore(Genre genre)
        {
            var taskOfVideos = TaskExtensions.Unwrap<IEnumerable<Movie>>(httpClient.GetAsync(serviceBaseAddress + "api/movies/" + genre.ToString())
                .ContinueWith(responseTask =>
                {
                    using (var httpResponseMessage = responseTask.Result)
                    {
                        ExceptionTransformerRest.ThrowIfNotSuccessStatusCode(httpResponseMessage);
                        return httpResponseMessage.Content.ReadAsAsync<IEnumerable<Movie>>(mediaTypeFormatters)
                            .ContinueWith(readTask => readTask.Result);
                    }
                }));

            try
            {
                return taskOfVideos.Result;
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
        }

        protected override IEnumerable<Movie> GetMoviesByYearCore(int year)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                if (httpClient != null)
                    httpClient.Dispose();
                disposed = true;
            }
        }
    }
}
