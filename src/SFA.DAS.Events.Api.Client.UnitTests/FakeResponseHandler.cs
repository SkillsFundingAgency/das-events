﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.Events.Api.Client.UnitTests
{
    public class FakeResponseHandler : DelegatingHandler
    {
        private readonly Dictionary<TestRequest, HttpResponseMessage> _fakeResponses = new Dictionary<TestRequest, HttpResponseMessage>();

        public void AddFakeResponse(TestRequest request, HttpResponseMessage responseMessage)
        {
            _fakeResponses.Add(request, responseMessage);
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var str = "";
            if (request?.Content != null) str = await request?.Content.ReadAsStringAsync();
            var testRequest = new TestRequest(request.RequestUri, str);

            if (_fakeResponses.ContainsKey(testRequest))
            {
                return _fakeResponses[testRequest];
            }
            else
            {
                Console.WriteLine($"----------------------");
                Console.WriteLine($"Test Request: {testRequest.Uri}");
                Console.WriteLine("Fake Responses: \n" + string.Join("\n", _fakeResponses.Select(m => m.Key.Uri)));
                
                return new HttpResponseMessage(HttpStatusCode.NotFound)
                           {
                               RequestMessage = request,
                               Content = new StringContent(string.Empty)
                           };
            }
        }
    }

    public class TestRequest
    {
        public TestRequest(Uri uri, string requestContent)
        {
            Uri = uri;
            RequestContent = requestContent;
        }

        public Uri Uri { get; private set; }
        public string RequestContent { get; private set; }

        public override int GetHashCode()
        {
            return Uri.ToString().GetHashCode() ^ RequestContent.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            TestRequest otherRequest;
            otherRequest = (TestRequest)obj;

            return (obj.GetHashCode() == otherRequest.GetHashCode());
        }
    }
}