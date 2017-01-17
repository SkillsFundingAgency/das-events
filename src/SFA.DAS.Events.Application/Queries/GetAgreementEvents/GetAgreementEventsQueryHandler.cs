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
        private readonly AbstractValidator<GetAgreementEventsRequest> _validator;

        public GetAgreementEventsQueryHandler(IAgreementEventRepository agreementEventRepository, AbstractValidator<GetAgreementEventsRequest> validator)
        {
            if (agreementEventRepository == null)
                throw new ArgumentNullException(nameof(agreementEventRepository));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            _agreementEventRepository = agreementEventRepository;
            _validator = validator;
        }

        public async Task<GetAgreementEventsResponse> Handle(GetAgreementEventsRequest request)
        {
            Validate(request);

            var events = await _agreementEventRepository.GetByRange(request.FromDateTime, request.ToDateTime, request.PageSize, request.PageNumber, request.FromEventId);

            return new GetAgreementEventsResponse {Data = events};
        }

        private void Validate(GetAgreementEventsRequest request)
        {
            _validator.ValidateAndThrow(request);
        }
    }
}
