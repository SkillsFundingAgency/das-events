﻿using NUnit.Framework;
using SFA.DAS.Events.Application.Commands.CreateApprenticeshipEvent;

namespace SFA.DAS.Events.Application.UnitTests.Commands.CreateApprenticeshipEventTests
{
    [TestFixture]
    public class WhenIValidateTheCommand
    {
        private CreateApprenticeshipEventCommandValidator _validator;
        private CreateApprenticeshipEventCommand _command;

        [SetUp]
        public void Arrange()
        {
            _validator = new CreateApprenticeshipEventCommandValidator();
            _command = new CreateApprenticeshipEventCommand();
        }

        [Test]
        public void ThenTransferSenderIsValidIfIdAndNameAreBothProvided()
        {
            _command.TransferSenderId = 123;
            _command.TransferSenderName = "Sender";

            var result = _validator.Validate(_command);

            Assert.IsTrue(result.IsValid);
        }

        [TestCase("")]
        [TestCase(null)]
        public void ThenTransferSenderIdMustNotBeProvidedWithoutName(string emptyValue)
        {
            _command.TransferSenderId = 123;
            _command.TransferSenderName = emptyValue;

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void ThenTransferSenderNameMustNotBeProvidedWithoutId()
        {
            _command.TransferSenderName = "Sender";

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void ThenTransferSenderIsValidIfAllDetailsAreProvidedWhenApproved()
        {
            _command.TransferSenderId = 123;
            _command.TransferSenderName = "Sender";
            _command.TransferSenderApproved = true;

            var result = _validator.Validate(_command);

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void ThenTransferSenderApprovedMustNotBeSetWithoutId()
        {
            _command.TransferSenderApproved = true;

            var result = _validator.Validate(_command);

            Assert.IsFalse(result.IsValid);
        }
    }
}
