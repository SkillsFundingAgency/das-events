using System;
using System.Threading.Tasks;
using FluentValidation;
using NUnit.Framework;
using SFA.DAS.Events.Application.UnitTests.Builders;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetApprenticeshipEventsTests
{
    [TestFixture]
    public class WhenIValidateTheRequest : GetApprenticeshipEventsTestBase
    {
        [Test]
        public void AndTheFromDateIsAfterTheToDateThenValidationFails()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().WithFromDate(DateTime.Now.AddDays(-1)).WithToDate(DateTime.Now.AddDays(-2)).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(request));
        }

        [Test]
        public void AndThePageNumberIsLessThanOneThenValidationFails()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().WithPageNumber(0).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(request));
        }

        [Test]
        public void AndThePageSizeIsLessThanOneThenValidationFails()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().WithPageSize(0).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(request));
        }

        [Test]
        public void AndThePageNumberIsGreaterThanTenThousandThenValidationFails()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().WithPageSize(10001).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(request));
        }

        [Test]
        public void AndTheEventIdIsLessThanZeroThenValidationFails()
        {
            var request = new GetApprenticeshipEventsRequestBuilder().WithEventId(-1).Build();

            Assert.ThrowsAsync<ValidationException>(() => Handler.Handle(request));
        }
    }
}
