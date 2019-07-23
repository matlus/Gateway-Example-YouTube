﻿using MovieClientApplication.Exceptions;
using MovieClientApplication.Gateways.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace MovieClientApplication.Gateways.ExceptionTransformers
{
    internal static class ExceptionTransformerRest
    {
        public static void ThrowIfNotSuccessStatusCode(HttpResponseMessage httpResponseMessage)
        {
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                var statusCode = httpResponseMessage.StatusCode;

                if ((int)statusCode >= 400 && (int)statusCode < 500)
                {
                    ThrowDomainException(httpResponseMessage);
                }
                else if ((int)statusCode >= 500)
                {
                    throw new InternalRemoteServerException(GetHttpContent(httpResponseMessage.Content));
                }
            }
        }

        private static void ThrowDomainException(HttpResponseMessage httpResponseMessage)
        {
            var reasonPhrase = httpResponseMessage.ReasonPhrase;
            var notices = GetNoticesFromHttpContent(httpResponseMessage.Content);
            var noticesMessage = string.Join("\r\n", notices);


            IEnumerable<string> exceptionTypeHeaders;
            httpResponseMessage.Headers.TryGetValues("Exception-Type", out exceptionTypeHeaders);
            var exceptionName = (exceptionTypeHeaders != null) ? exceptionTypeHeaders.First() : string.Empty;

            if (exceptionName != null)
            {
                switch (exceptionName)
                {
                    case "InvalidGenreException":
                        throw new InvalidGenreException(reasonPhrase + "\r\n" + noticesMessage);
                    default:
                        break;
                }
            }
        }

        private static string GetHttpContent(HttpContent httpContent)
        {
            using (var stream = httpContent.ReadAsStreamAsync().ContinueWith(readTask => readTask.Result).Result)
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static IEnumerable<Notice> GetNoticesFromHttpContent(HttpContent httpContent)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Notice>>(GetHttpContent(httpContent));
        }
    }
}
