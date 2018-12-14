using System;

namespace SFA.DAS.Events.Api.Client.Configuration
{
    public interface IEventsApiClientConfiguration
    {
        string BaseUrl { get; }
        
        string ClientId { get; }
        string ClientSecret { get; }
        string IdentifierUri { get; }
        string Tenant { get; }
        
        [Obsolete("Jwt token usage is obsolete. Use Azure AD authentication.")]
        string ClientToken { get; }
    }
}