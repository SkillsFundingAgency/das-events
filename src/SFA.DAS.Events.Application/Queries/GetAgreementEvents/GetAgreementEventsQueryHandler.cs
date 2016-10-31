using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SFA.DAS.Events.Domain.Repositories;

namespace SFA.DAS.Events.Application.Queries.GetAgreementEvents
{
    public sealed class GetAgreementEventsQueryHandler : IAsyncRequestHandler<GetAgreementEventsRequest, GetAgreementEventsResponse>
    {
        private readonly IAgreementEventRepository _agreementEventRepository;

        public GetAgreementEventsQueryHandler(IAgreementEventRepository agreementEventRepository)
        {
            _agreementEventRepository = agreementEventRepository;
        }

        public async Task<GetAgreementEventsResponse> Handle(GetAgreementEventsRequest request)
        {
            Validate(request);

            var events = await _agreementEventRepository.GetByDateRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId);

            return new GetAgreementEventsResponse {Data = events};
        }

        private static void Validate(GetAgreementEventsRequest request)
        {
            var validator = new GetAgreementEventsRequestValidator();

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
