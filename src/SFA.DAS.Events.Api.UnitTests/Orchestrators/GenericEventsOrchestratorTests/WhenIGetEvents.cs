using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Application.Queries.GetGenericEvents;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.GenericEventsOrchestratorTests
{
    public class WhenIGetEvents
    {
        private GenericEventOrchestrator _orchestrator;
        private Mock<IEventsLogger> _eventsLogger;
        private Mock<IMediator> _mediator;
        private IEnumerable<GenericEvent> _events;

        [SetUp]
        public void Arrange()
        {
            _mediator = new Mock<IMediator>();
            _eventsLogger = new Mock<IEventsLogger>();
            _orchestrator = new GenericEventOrchestrator(_mediator.Object);

            _events = new List<GenericEvent>();

            _mediator.Setup(x => x.SendAsync(It.IsAny<GetGenericEventsRequest>()))
                .ReturnsAsync(() => new GetGenericEventsResponse
                {
                    Data = _events
                });
        }
        
        [Test]
        public async Task ThenIShouldGetAllEvents()
        {
            //Act
            await _orchestrator.GetEvents("Test", DateTime.MinValue, DateTime.MaxValue, 100, 1, 1);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.IsAny<GetGenericEventsRequest>()), Times.Once);
        }

        [Test]
        public async Task ThenIShouldGetAllEventsOfAllRequestedTypes()
        {
            //Arrange
            var eventTypes = new[] {"EventOne", "EventTwo"};

            //Act
            await _orchestrator.GetEvents(eventTypes, DateTime.MinValue, DateTime.MaxValue, 100, 1, 1);

            //Assert
            _mediator.Verify(x => x.SendAsync(It.Is<GetGenericEventsRequest>(
                y => y.EventTypes.SequenceEqual(eventTypes))), Times.Once);
        }
    }
}
