using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetGenericEventsSinceEvent;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetGenericEventsTests
{
    class WhenIGetGenericEvents
    {
        private Mock<IGenericEventRepository> _repository;
        private GetGenericEventsSinceEventQueryHandler _handler;
        private IValidator<GetGenericEventsSinceEventRequest> _validator;
        private ICollection<GenericEvent> _events;
        private GetGenericEventsSinceEventRequest _sinceEventRequest;

        [SetUp]
        public void Arrange()
        {
            _repository = new Mock<IGenericEventRepository>();
            _validator = new GetGenericEventsSinceEventRequestValidator();

            _events = new List<GenericEvent>
            {
                new GenericEvent()
            };

            _sinceEventRequest = new GetGenericEventsSinceEventRequest
            {
                EventTypes = new[] {"test", "test2"},
                FromDateTime = DateTime.Now,
                ToDateTime = DateTime.Now,
                PageNumber = 1,
                PageSize = 1000,
                FromEventId = 0
            };

            _handler = new GetGenericEventsSinceEventQueryHandler(_repository.Object, _validator);

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
            var response = await _handler.Handle(_sinceEventRequest);

            //Assert
            _repository.Verify(x => x.GetByDateRange(
                _sinceEventRequest.EventTypes, 
                _sinceEventRequest.FromDateTime, 
                _sinceEventRequest.ToDateTime, 
                _sinceEventRequest.PageSize, 
                _sinceEventRequest.PageNumber), Times.Once);

            Assert.AreEqual(_events, response.Data);
        }

        [Test]
        public void ThenAnExceptionShouldBeThrowIfTheRequestIsInvalid()
        {
            //Arrange
            _sinceEventRequest.EventTypes = new string[1];

            ////Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => { await _handler.Handle(_sinceEventRequest); });
        }
    }
}
