using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SFA.DAS.Events.Api.Client.Configuration;
using SFA.DAS.Http;

namespace SFA.DAS.Events.Api.Client
{
    public partial class EventsApi : RestHttpClient
    {
        private readonly IEventsApiClientConfiguration _configuration;

        public EventsApi(HttpClient client, IEventsApiClientConfiguration configuration)
            : base(client)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public EventsApi(IEventsApiClientConfiguration configuration) : this(new HttpClient(), configuration)
        {
        }

        private Task PostEvent<T>(string url, T @event)
        {
            return PostAsJson(url, @event);
        }

        private Task<List<T>> GetEvents<T>(string url)
        {
            return Get<List<T>>(url);
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
