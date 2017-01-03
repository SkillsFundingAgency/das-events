using System;
using System.Web.Http;
using SFA.DAS.NLog.Logger;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure;

namespace SFA.DAS.Events.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ILog Logger = new NLogLogger();

        protected void Application_Start()
        {
            Logger.Info("Starting Events Api Application");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            TelemetryConfiguration.Active.InstrumentationKey = CloudConfigurationManager.GetSetting("InstrumentationKey");
        }

        protected void Application_End()
        {
            Logger.Info("Stopping Events Api Application");
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError().GetBaseException();

            Logger.Error(ex, "Unhandled exception");
        }
    }
}
