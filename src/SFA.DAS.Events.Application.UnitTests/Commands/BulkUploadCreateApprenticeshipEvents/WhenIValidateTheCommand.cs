using System;
using System.Collections.Generic;
using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.BulkUploadCreateApprenticeshipEvents;
using SFA.DAS.Events.Domain.Entities;

namespace SFA.DAS.Events.Application.UnitTests.Commands.BulkUploadCreateApprenticeshipEvents
{
    [TestFixture]
    public class WhenIValidateTheCommand
    {
        private BulkUploadCreateApprentieshipEventsCommandValidator _validator;
        private BulkUploadCreateApprenticeshipEventsCommand _command;
        private ApprenticeshipEvent _apprenticeshipEvent;

        [SetUp]
        public void Arrange()
        {
            _validator = new BulkUploadCreateApprentieshipEventsCommandValidator();
            _apprenticeshipEvent = new ApprenticeshipEvent();
            _command = new BulkUploadCreateApprenticeshipEventsCommand
            {
                ApprenticeshipEvents = new List<ApprenticeshipEvent>()
            };
            _command.ApprenticeshipEvents.Add(_apprenticeshipEvent);
        }

        [Test]
        public void ThenTransferSenderIsValidIfIdAndNameAreBothProvided()
        {
            _apprenticeshipEvent.TransferSenderId = 123;
            _apprenticeshipEvent.TransferSenderName = "Sender";

            var result = _validator.Validate(_command);

            Assert.IsTrue(result.IsValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void ThenTransferSenderIdMustNotBeProvidedWithoutName(string emptyValue)
        {
            _apprenticeshipEvent.TransferSenderId = 123;
            _apprenticeshipEvent.TransferSenderName = emptyValue;

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void ThenTransferSenderNameMustNotBeProvidedWithoutId()
        {
            _apprenticeshipEvent.TransferSenderName = "Sender";

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void ThenTransferSenderIsValidIfAllDetailsAreProvidedWhenApproved()
        {
            _apprenticeshipEvent.TransferSenderId = 123;
            _apprenticeshipEvent.TransferSenderName = "Sender";
            _apprenticeshipEvent.TransferApprovalStatus = TransferApprovalStatus.TransferApproved;
            _apprenticeshipEvent.TransferApprovalActionedOn = DateTime.Now;

            var result = _validator.Validate(_command);

            Assert.IsTrue(result.IsValid);
        }

        [TestCase(TransferApprovalStatus.TransferApproved)]
        [TestCase(TransferApprovalStatus.Pending)]
        [TestCase(TransferApprovalStatus.TransferRejected)]
        public void ThenTransferSenderApprovedMustNotBeSetWithoutId(TransferApprovalStatus status)
        {
            _apprenticeshipEvent.TransferSenderId = null;
            _apprenticeshipEvent.TransferApprovalStatus = status;

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }
    }
}
