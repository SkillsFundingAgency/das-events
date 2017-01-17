using System;

namespace SFA.DAS.Events.Application.UnitTests.Builders
{
    internal abstract class GetEventRequestBuilder<T>
    {
        protected DateTime FromDateTime = DateTime.Now.AddDays(-30);
        protected int FromEventId = 123;
        protected int PageNumber = 2;
        protected int PageSize = 1000;
        protected DateTime ToDateTime = DateTime.Now;

        internal GetEventRequestBuilder<T> WithFromDate(DateTime fromDate)
        {
            FromDateTime = fromDate;
            return this;
        }

        internal GetEventRequestBuilder<T> WithToDate(DateTime toDate)
        {
            ToDateTime = toDate;
            return this;
        }

        internal GetEventRequestBuilder<T> WithPageNumber(int pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }

        internal GetEventRequestBuilder<T> WithPageSize(int pageSize)
        {
            PageSize = pageSize;
            return this;
        }

        internal GetEventRequestBuilder<T> WithEventId(int eventId)
        {
            FromEventId = eventId;
            return this;
        }

        internal abstract T Build();
    }
}
