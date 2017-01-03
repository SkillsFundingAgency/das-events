using System;
using System.Collections.Generic;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.Events.Infrastructure.Logging
{
    public class EventsLogger : IEventsLogger
    {
        private readonly ILog _logger;

        public EventsLogger(ILog logger)
        {
            _logger = logger;
        }

        public void Trace(string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Trace(message, properties);
        }

        public void Debug(string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Debug(message, properties);
        }

        public void Info(string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Info(message, properties);
        }

        public void Warn(string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Warn(message, properties);
        }

        public void Warn(Exception ex, string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Warn(ex, message, properties);
        }

        public void Error(Exception ex, string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Error(ex, message, properties);
        }

        public void Fatal(Exception ex, string message, string accountId = null, string providerId = null, string @event = null)
        {
            IDictionary<string, object> properties = BuildPropertyDictionary(accountId, providerId, @event);
            _logger.Fatal(ex, message, properties);
        }

        private IDictionary<string, object> BuildPropertyDictionary(string accountId, string providerId, string @event)
        {
            var properties = new Dictionary<string, object>();

            if (!string.IsNullOrWhiteSpace(accountId)) properties.Add("AccountId", accountId);
            if (!string.IsNullOrWhiteSpace(providerId)) properties.Add("ProviderId", providerId);
            if (!string.IsNullOrWhiteSpace(@event)) properties.Add("Event", @event);

            return properties;
        }
    }
}
