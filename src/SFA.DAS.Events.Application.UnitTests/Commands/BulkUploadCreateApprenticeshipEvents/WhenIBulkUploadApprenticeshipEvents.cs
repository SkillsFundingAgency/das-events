using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.BulkUploadCreateApprenticeshipEvents
{
    [TestFixture]
    public class WhenIBulkUploadApprenticeshipEvents : BulkUploadCreateApprenticeshipsEventTestBase
    {
        [Test]
        public async Task ThenTheRequestIsValidated()
        {
            await Handler.Handle(new BulkUploadCreateApprenticeshipEventsCommand
            {
                ApprenticeshipEvents = new List<ApprenticeshipEvent>()
            });

            Validator.Verify(x => x.Validate(It.IsAny<BulkUploadCreateApprenticeshipEventsCommand>()), Times.Once);
        }
    }
}
