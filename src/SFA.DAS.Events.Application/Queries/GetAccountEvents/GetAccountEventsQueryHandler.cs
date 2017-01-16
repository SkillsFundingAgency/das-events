using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetAccountEvents
{
    public sealed class GetAccountEventsQueryHandler : IAsyncRequestHandler<GetAccountEventsRequest, GetAccountEventsResponse>
    {
        private readonly IAccountEventRepository _accountEventRepository;
        private readonly AbstractValidator<GetAccountEventsRequest> _validator;

        public GetAccountEventsQueryHandler(IAccountEventRepository accountEventRepository, AbstractValidator<GetAccountEventsRequest> validator)
        {
            if (accountEventRepository == null)
                throw new ArgumentNullException(nameof(accountEventRepository));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            _accountEventRepository = accountEventRepository;
            _validator = validator;
        }

        public async Task<GetAccountEventsResponse> Handle(GetAccountEventsRequest request)
        {
            Validate(request);

            var events = await _accountEventRepository.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId);

            return new GetAccountEventsResponse {Data = events};
        }

        private void Validate(GetAccountEventsRequest request)
        {
            _validator.ValidateAndThrow(request);
        }
    }
}
