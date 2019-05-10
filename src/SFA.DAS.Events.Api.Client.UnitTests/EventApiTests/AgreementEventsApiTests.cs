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
    public class AgreementEventsApiTests
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
        public async Task CreateAgreementEvent()
        {
            var input = new AgreementEvent();
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + "api/events/engagements"), JsonConvert.SerializeObject(input));
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(string.Empty) });

            await _sut.CreateAgreementEvent(input);

            Assert.Pass();
        }

        [Test]
        public async Task GetAgreementEventsById()
        {
            long fromEventId = 1;
            int pageSize = 200;
            int pageNumber = 2;
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/engagements?fromEventId={fromEventId}&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);

            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent<List<AccountEventView>>(new List<AccountEventView>())
            });

            await _sut.GetAgreementEventsById(fromEventId, pageSize, pageNumber);

            Assert.Pass();
        }

        [Test]
        public async Task GetAgreementEventsByDateRange()
        {
            DateTime? fromDate = System.DateTime.Parse("2017-05-01");
            DateTime? toDate = DateTime.Parse("2017-12-08");
            int pageSize = 1000;
            int pageNumber = 1;
            
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/engagements?fromDate=20170501000000&toDate=20171208000000&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent<List<AccountEventView>>(new List<AccountEventView>())
            });

            await _sut.GetAgreementEventsByDateRange(fromDate, toDate, pageSize, pageNumber);

            Assert.Pass();
        }
    }
}