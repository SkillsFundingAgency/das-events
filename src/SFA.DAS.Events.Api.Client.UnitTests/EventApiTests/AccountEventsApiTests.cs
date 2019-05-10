using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Moq;

using Newtonsoft.Json;

using NUnit.Framework;
using SFA.DAS.Events.Api.Client.Configuration;
using SFA.DAS.Events.Api.Types;

namespace SFA.DAS.Events.Api.Client.UnitTests.EventApiTests
{
    [TestFixture]
    public class AccountEventsApiTests
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
        public async Task CreateAccountEvent()
        {
            var input = new AccountEvent();
            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/accounts"), JsonConvert.SerializeObject(input));
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(string.Empty) });

            await _sut.CreateAccountEvent(input);

            Assert.Pass();
        }

        [Test]
        public async Task GetAccountEventsById()
        {
            long fromEventId = 1;
            int pageSize = 200;
            int pageNumber = 2;

            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/accounts?fromEventId={fromEventId}&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent<List<AccountEventView>>(new List<AccountEventView>())
            });

            await _sut.GetAccountEventsById(fromEventId, pageSize, pageNumber);

            Assert.Pass();
        }

        [Test]
        public async Task GetAccountEventsByDateRange()
        {
            DateTime? fromDate = System.DateTime.Parse("2017-05-01");
            DateTime? toDate = DateTime.Parse("2017-12-08");
            int pageSize = 1000;
            int pageNumber = 1;

            var employerRequest = new TestRequest(new Uri(ExpectedApiBaseUrl + $"api/events/accounts?fromDate=20170501000000&toDate=20171208000000&pageSize={pageSize}&pageNumber={pageNumber}"), string.Empty);
            _fakeHandler.AddFakeResponse(employerRequest, new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent<List<AccountEventView>>(new List<AccountEventView>())
            });

            await _sut.GetAccountEventsByDateRange(fromDate, toDate, pageSize, pageNumber);

            Assert.Pass();
        }
    }


    internal class JsonContent<TContentType> : StringContent
    {
        public JsonContent(TContentType content) : base(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
        {
            // just call base   
        }
    }
}