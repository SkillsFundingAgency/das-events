using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Queries.GetGenericEventsByResourceId;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.UnitTests.Queries.GetGenericEventsTests
{
    class WhenIGetGenericEventsByResourceId
    {
        private Mock<IGenericEventRepository> _repository;
        private GetGenericEventsByResourceIdQueryHandler _handler;
        private ICollection<GenericEvent> _events;
        private GetGenericEventsByResourceIdRequest _request;

        [SetUp]
        public void Arrange()
        {
            _repository = new Mock<IGenericEventRepository>();
            
            _events = new List<GenericEvent>
            {
                new GenericEvent()
            };

            _request = new GetGenericEventsByResourceIdRequest()
            {
                ResourceType = "Type",
                ResourceId = "Id",
                FromDateTime = DateTime.Now,
                ToDateTime = DateTime.Now,
                PageNumber = 1,
                PageSize = 1000
            };

            _handler = new GetGenericEventsByResourceIdQueryHandler(_repository.Object);

            _repository.Setup(x => x.GetByResourceId(
                    It.IsAny<string>(),
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
            _repository.Verify(x => x.GetByResourceId(
                _request.ResourceType,
                _request.ResourceId,
                _request.FromDateTime,
                _request.ToDateTime,
                _request.PageSize,
                _request.PageNumber), Times.Once);

            Assert.AreEqual(_events, response.Data);
        }
    }
}
