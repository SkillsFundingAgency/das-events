using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceUri;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetGenericEventsTests
{
    class WhenIGetGenericEventsByResourceUri
    {
        private Mock<IGenericEventRepository> _repository;
        private GetGenericEventsByResourceUriQueryHandler _handler;
        private ICollection<GenericEvent> _events;
        private GetGenericEventsByResourceUriRequest _request;

        [SetUp]
        public void Arrange()
        {
            _repository = new Mock<IGenericEventRepository>();
            
            _events = new List<GenericEvent>
            {
                new GenericEvent()
            };

            _request = new GetGenericEventsByResourceUriRequest()
            {
                ResourceUri = "Uri",
                FromDateTime = DateTime.Now,
                ToDateTime = DateTime.Now,
                PageNumber = 1,
                PageSize = 1000
            };

            _handler = new GetGenericEventsByResourceUriQueryHandler(_repository.Object);

            _repository.Setup(x => x.GetByResourceUri(
                    It.IsAny<string>(),
                    It.IsAny<DateTime?>(),
                    It.IsAny<DateTime?>(),
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
            _repository.Verify(x => x.GetByResourceUri(
                _request.ResourceUri,
                _request.FromDateTime,
                _request.ToDateTime,
                _request.PageSize,
                _request.PageNumber), Times.Once);

            Assert.AreEqual(_events, response.Data);
        }
    }
}
