using SFA.DAS.Http.Configuration;

namespace SFA.DAS.Events.Api.Client.Configuration
{
    public interface IEventsApiClientConfiguration : IAzureActiveDirectoryClientConfiguration, IJwtClientConfiguration
    {
        string BaseUrl { get; }
    }
}