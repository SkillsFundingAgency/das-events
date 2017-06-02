using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByDateRange;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetGenericEventsTests
{
    class WhenIGetGenericEventsByDateRange
    {
        private Mock<IGenericEventRepository> _repository;
        private GetGenericEventsByDateRangeQueryHandler _handler;
        private IValidator<GetGenericEventsByDateRangeRequest> _validator;
        private ICollection<GenericEvent> _events;
        private GetGenericEventsByDateRangeRequest _request;

        [SetUp]
        public void Arrange()
        {
            _repository = new Mock<IGenericEventRepository>();
            _validator = new GetGenericEventsByDateRangeRequestValidator();

            _events = new List<GenericEvent>
            {
                new GenericEvent()
            };

            _request = new GetGenericEventsByDateRangeRequest()
            {
                EventTypes = new[] {"test", "test2"},
                FromDateTime = DateTime.Now,
                ToDateTime = DateTime.Now,
                PageNumber = 1,
                PageSize = 1000,
                FromEventId = 0
            };

            _handler = new GetGenericEventsByDateRangeQueryHandler(_repository.Object, _validator);

            _repository.Setup(x => x.GetByDateRange(
                    It.IsAny<IEnumerable<string>>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<DateTime>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
                .ReturnsAsync(_events);
        }

        [Test]
        public async Task ThenTheRepositoryShouldBeCalled()
        {
            //Act
            var response = await _handler.Handle(_request);

            //Assert
            _repository.Verify(x => x.GetByDateRange(
                _request.EventTypes,
                _request.FromDateTime,
                _request.ToDateTime,
                _request.PageSize,
                _request.PageNumber), Times.Once);

            Assert.AreEqual(_events, response.Data);
        }

        [Test]
        public void ThenAnExceptionShouldBeThrowIfTheRequestIsInvalid()
        {
            //Arrange
            _request.EventTypes = new string[1];

            ////Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => { await _handler.Handle(_request); });
        }
    }
}
