using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieService.Controllers;
using Movies.DomainLayer;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Movies.DomainLayer.Managers.Exceptions;
using System.Collections.Generic;
using System.Net;

namespace AcceptanceTests
{
    [TestClass]
    public class MoviesControllerTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            JsonConvert.DefaultSettings = (() =>
                {
                    var jsonSerializerSettings = new JsonSerializerSettings();
                    jsonSerializerSettings.Converters.Add(new StringEnumConverter());
                    return jsonSerializerSettings;
                });
        }

        [TestMethod]
        [TestCategory("Acceptance Test")]
        public void MoviesController_GetByGenre_WhenGenreIsNotValid_ShouldThrow()
        {
            var moviesController = new MoviesControllerForTest();
            var expectedProperty = "genre";
            var expectedMessagePart = "";


            try
            {
                var invalidGenre = "xxx";
                moviesController.Get(invalidGenre);
                Assert.Fail("A HttpResponseException was expected, but no exception was thrown");
            }
            catch (HttpResponseException e)
            {
                Assert.AreEqual(HttpStatusCode.BadRequest, e.Response.StatusCode);
                Assert.AreEqual("Invalid Genre Exception", e.Response.ReasonPhrase);

                using (var reader = new StreamReader(e.Response.Content.ReadAsStreamAsync().Result))
                {
                    var content = reader.ReadToEnd();
                    var notices = JsonConvert.DeserializeObject<IEnumerable<Notice>>(content);
                    Assert.AreEqual(1, notices.Count(), "Expecting exactly one Notice item in the Notices collection");

                    var notice = notices.First();
                    Assert.AreEqual(Severity.Error, notice.Severity);
                    Assert.AreEqual(expectedProperty, notice.Property);
                    Assert.IsTrue(notice.Message.Contains(expectedMessagePart), "The Notice.Message was expected to contain: " + expectedMessagePart + ", but it did not. The actual Message was: " + notice.Message);
                }
            }
        }
    }

    class MoviesControllerForTest : MoviesController
    {
        protected override Movies.DomainLayer.DomainFacade MakeDomainFacade()
        {
            return new DomainFacade();
        }
    }
}