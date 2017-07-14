using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.Events.Api.Client.Configuration;
using SFA.DAS.Http;

namespace SFA.DAS.Events.Api.Client
{
    public partial class EventsApi : ApiClientBase
    {
        private readonly IEventsApiClientConfiguration _configuration;

        public EventsApi(HttpClient client, IEventsApiClientConfiguration configuration)
            : base(client)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            _configuration = configuration;
        }

        public EventsApi(IEventsApiClientConfiguration configuration) : this(new HttpClient(), configuration)
        {
        }

        private async Task PostEvent<T>(string url, T @event)
        {
            var data = JsonConvert.SerializeObject(@event);

            await PostAsync(url, data);
        }

        private async Task<List<T>> GetEvents<T>(string url)
        {
            var content = await GetAsync(url);

            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        private static string BuildDateQuery(DateTime? fromDate, DateTime? toDate)
        {
            var fromDateString = FormatDateTime(fromDate);
            var toDateString = FormatDateTime(toDate);

            if (string.IsNullOrWhiteSpace(fromDateString))
                return string.IsNullOrWhiteSpace(toDateString) ? string.Empty : $"toDate={toDateString}&";

            return string.IsNullOrWhiteSpace(toDateString)
                ? $"fromDate={fromDateString}&"
                : $"fromDate={fromDateString}&toDate={toDateString}&";
        }

        private static string FormatDateTime(DateTime? source)
        {
            return source.HasValue ? $"{source:yyyyMMddHHmmss}" : string.Empty;
        }
    }
}
