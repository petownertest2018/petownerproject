using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetOwnerApiClient.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PetOwnerUnitTest
{
    [TestClass]
    public class PetOwnerApiServiceTest
    {
        [TestMethod]
        public void Should_return_result_json_when_api_service_return_success()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
             var httpClient = new HttpClient(fakeHttpMessageHandler.Object); 
            fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{}")
            });
            var apiService = new PetOwnerApiService(httpClient, new Uri("http://localhost"));
            var actual = apiService.GetPetOwnerJson().Result;
            Assert.AreEqual("{}", actual.PetOwnerJson);
        }

        [TestMethod]
        public void Should_return_error_json_when_api_service_does_not_return_success()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(fakeHttpMessageHandler.Object);
            fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadGateway,
                Content = new StringContent("{}")
            });
            var apiService = new PetOwnerApiService(httpClient,new Uri("http://localhost"));
            var actual = apiService.GetPetOwnerJson().Result;
            Assert.AreEqual("Pet Owner Api could not respond with result.", actual.Error);
        }

        [TestMethod]
        public void Should_return_error_json_when_api_service_throws_exception()
        {
            var fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            var httpClient = new HttpClient(fakeHttpMessageHandler.Object);
            fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).
                Throws(new HttpRequestException());

            var apiService = new PetOwnerApiService(httpClient, new Uri("http://localhost"));
            var actual = apiService.GetPetOwnerJson().Result;
            Assert.AreEqual("Error occured while communicating to Pet Owner Api.", actual.Error);
        }
    }
}
