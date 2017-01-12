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

        public GetAccountEventsQueryHandler(IAccountEventRepository accountEventRepository)
        {
            _accountEventRepository = accountEventRepository;
        }

        public async Task<GetAccountEventsResponse> Handle(GetAccountEventsRequest request)
        {
            Validate(request);

            var events = await _accountEventRepository.GetByDateRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId);

            return new GetAccountEventsResponse {Data = events};
        }

        private static void Validate(GetAccountEventsRequest request)
        {
            var validator = new GetAccountEventsRequestValidator();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
