using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Entities;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Commands.CreateGenericEvent
{
    public class CreateGenericEventCommandHandler : AsyncRequestHandler<CreateGenericEventCommand>
    {
        private readonly IGenericEventRepository _repository;
        private readonly AbstractValidator<CreateGenericEventCommand> _validator;
        private readonly IEventsLogger _logger;

        public CreateGenericEventCommandHandler(IGenericEventRepository repository,
            AbstractValidator<CreateGenericEventCommand> validator,
            IEventsLogger logger)
        {
            _repository = repository;
            _validator = validator;
            _logger = logger;
        }

        protected override async Task HandleCore(CreateGenericEventCommand command)
        {
            _logger.Info($"Creating Generic Event of type {command.Type}");

            _validator.ValidateAndThrow(command);

            var genericEvent = new GenericEvent
            {
                Type = command.Type,
                Payload = command.Payload
            };

            try
            {
                await _repository.Create(genericEvent);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error storing generic event in database");
                throw;
            }
        }
    }
}
