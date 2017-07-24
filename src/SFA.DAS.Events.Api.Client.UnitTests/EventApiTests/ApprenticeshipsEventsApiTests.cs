using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

using SFA.DAS.Events.Api.Client.Configuration;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Client.UnitTests.EventApiTests
{
    [TestFixture]
    public class ApprenticeshipsEventsApiTests
    {
        private EventsApi _sut;
        private FakeResponseHandler _fakeHandler;
        private const string ExpectedApiBaseUrl = "http://test.local.url/";

        [SetUp]
        public void Arrange()
        {
            _fakeHandler = new FakeResponseHandler();
            var client = new HttpClient(_fakeHandler);
            var config = new Mock<IEventsApiClientConfiguration>();
            config.Setup(m => m.BaseUrl).Returns(ExpectedApiBaseUrl);

            _sut = new EventsApi(client, config.Object);
        }

        [Test]
        public async Task CreateApprenticeshipEvent()
        {
            var input = new ApprenticeshipEvent();
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/apprenticeships"), JsonConvert.SerializeObject(input));
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(string.Empty) });
            await _sut.CreateApprenticeshipEvent(input);

            Assert.Pass();
        }

        [Test]
        public async Task BulkCreateApprenticeshipEvent()
        {
            var input = new List<ApprenticeshipEvent>();
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/apprenticeships/bulk"), JsonConvert.SerializeObject(input));
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(string.Empty) });

            await _sut.BulkCreateApprenticeshipEvent(input);

            Assert.Pass();
        }

        [Test]
        public async Task GetApprenticeshipEventsById()
        {
            long fromEventId = 2;
            int pageSize = 20;
            int pageNumber = 3;
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/apprenticeships?fromEventId={fromEventId}&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(JsonConvert.SerializeObject(new List<ApprenticeshipEventView>())) });

            var result = await _sut.GetApprenticeshipEventsById(fromEventId, pageSize, pageNumber);

            Assert.Pass();
        }

        [Test]
        public async Task GetApprenticeshipEventsByDateRange()
        {
            DateTime? fromDate = System.DateTime.Parse("2017-05-01");
            DateTime? toDate = DateTime.Parse("2017-12-08");
            int pageSize = 1000;
            int pageNumber = 1;
            
            var dateString = "hej";
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/apprenticeships?fromDate=20170501000000&toDate=20171208000000&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new List<ApprenticeshipEventView>())) });

            var apprenticeshipEventList = await _sut.GetApprenticeshipEventsByDateRange(fromDate, toDate, pageSize, pageNumber);

            Assert.Pass();
        }


    }
}
