using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Api.Orchestrators;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceUri;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Api.UnitTests.Orchestrators.GenericEventsOrchestratorTests
{
    public class WhenIGetEventsByResourceUri
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

            _mediator.Setup(x => x.SendAsync(It.IsAny<GetGenericEventsByResourceUriRequest>()))
                .ReturnsAsync(() => new GetGenericEventsByResourceUriResponse
                {
                    Data = _events
                });
        }
        
        [Test]
        public async Task ThenIShouldGetAllEvents()
        {
            //Arrange
            var resourceUri = "URI";
            var fromDate = DateTime.Now.AddDays(-1);
            var toDate = DateTime.Now.AddDays(1);
            var pageNumber = 3;
            var pageSize = 50;

            //Act
            var response = await _orchestrator.GetEventsByResourceUri(resourceUri, fromDate, toDate, pageSize, pageNumber);

            //Assert
            _mediator.Verify(
                x =>
                    x.SendAsync(
                        It.Is<GetGenericEventsByResourceUriRequest>(
                            r =>
                                r.ResourceUri == resourceUri && r.FromDateTime == fromDate && r.ToDateTime == toDate && r.PageNumber == pageNumber &&
                                r.PageSize == pageSize)), Times.Once);

            response.ShouldAllBeEquivalentTo(_events);
        }
    }
}
