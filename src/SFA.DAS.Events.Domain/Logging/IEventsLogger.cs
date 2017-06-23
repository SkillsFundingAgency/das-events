using SFA.DAS.NLog.Logger;
using System;

namespace SFA.DAS.Events.Domain.Logging
{
    public interface IEventsLogger
    {
        ILog BaseLogger { get; }
        void Trace(string message, string accountId = null, string providerId = null, string @event = null);
        void Debug(string message, string accountId = null, string providerId = null, string @event = null);
        void Info(string message, string accountId = null, string providerId = null, string @event = null);
        void Warn(string message, string accountId = null, string providerId = null, string @event = null);
        void Warn(Exception ex, string message, string accountId = null, string providerId = null, string @event = null);
        void Error(Exception ex, string message, string accountId = null, string providerId = null, string @event = null);
        void Fatal(Exception ex, string message, string accountId = null, string providerId = null, string @event = null);
    }
}
