using System;
using SFA.DAS.Events.Application.Queries.GetAccountEvents;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    public class GetAccountEventsRequestBuilder
    {
        private DateTime _fromDateTime = DateTime.Now.AddDays(-30);
        private int _fromEventId = 123;
        private int _pageNumber = 2;
        private int _pageSize = 1000;
        private DateTime _toDateTime = DateTime.Now;

        public GetAccountEventsRequest Build()
        {
            return new GetAccountEventsRequest
            {
                FromDateTime = _fromDateTime,
                FromEventId = _fromEventId,
                PageNumber = _pageNumber,
                PageSize = _pageSize,
                ToDateTime = _toDateTime
            };
        }

        public GetAccountEventsRequestBuilder WithFromDate(DateTime fromDate)
        {
            _fromDateTime = fromDate;
            return this;
        }

        public GetAccountEventsRequestBuilder WithToDate(DateTime toDate)
        {
            _toDateTime = toDate;
            return this;
        }

        public GetAccountEventsRequestBuilder WithPageNumber(int pageNumber)
        {
            _pageNumber = pageNumber;
            return this;
        }

        public GetAccountEventsRequestBuilder WithPageSize(int pageSize)
        {
            _pageSize = pageSize;
            return this;
        }

        public GetAccountEventsRequestBuilder WithEventId(int eventId)
        {
            _fromEventId = eventId;
            return this;
        }
    }
}
