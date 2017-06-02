using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.GenericEventsOrchestratorTests
{
    public class WhenIGetEventsByResourceId
    {
        private GenericEventOrchestrator _orchestrator;
        private Mock<IMediator> _mediator;
        private IEnumerable<GenericEvent> _events;

        [SetUp]
        public void Arrange()
        {
            _mediator = new Mock<IMediator>();
            _orchestrator = new GenericEventOrchestrator(_mediator.Object);

            _events = new List<GenericEvent> { new GenericEvent { CreatedOn = DateTime.Now.AddDays(-1), Id = 123, Type = "EventType", Payload = "ld;kjfdlkjfnfdjgfdvg" } };

            _mediator.Setup(x => x.SendAsync(It.IsAny<GetGenericEventsByResourceIdRequest>()))
                .ReturnsAsync(() => new GetGenericEventsByResourceIdResponse
                {
                    Data = _events
                });
        }
        
        [Test]
        public async Task ThenIShouldGetAllEvents()
        {
            //Arrange
            var resourceType = "Type";
            var resourceId = "ID";
            var fromDate = DateTime.Now.AddDays(-1);
            var toDate = DateTime.Now.AddDays(1);
            var pageNumber = 3;
            var pageSize = 50;

            //Act
            var response = await _orchestrator.GetEventsByResourceId(resourceType, resourceId, fromDate, toDate, pageSize, pageNumber);

            //Assert
            _mediator.Verify(
                x =>
                    x.SendAsync(
                        It.Is<GetGenericEventsByResourceIdRequest>(
                            r =>
                                r.ResourceType == resourceType && r.ResourceId == resourceId && r.FromDateTime == fromDate && r.ToDateTime == toDate && r.PageNumber == pageNumber &&
                                r.PageSize == pageSize)), Times.Once);

            response.ShouldAllBeEquivalentTo(_events);
        }
    }
}
