using System;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    public abstract class GetEventRequestBuilder<T>
    {
        protected DateTime FromDateTime = DateTime.Now.AddDays(-30);
        protected int FromEventId = 123;
        protected int PageNumber = 2;
        protected int PageSize = 1000;
        protected DateTime ToDateTime = DateTime.Now;

        public GetEventRequestBuilder<T> WithFromDate(DateTime fromDate)
        {
            FromDateTime = fromDate;
            return this;
        }

        public GetEventRequestBuilder<T> WithToDate(DateTime toDate)
        {
            ToDateTime = toDate;
            return this;
        }

        public GetEventRequestBuilder<T> WithPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }

        public GetEventRequestBuilder<T> WithPageSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }

        public GetEventRequestBuilder<T> WithEventId(int eventId)
        {
            FromEventId = eventId;
            return this;
        }

        public abstract T Build();
    }
}
