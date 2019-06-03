using System;
using System.Web.Http;
using SFA.DAS.NLog.Logger;
using Microsoft.ApplicationInsights.Extensibility;
using System.Configuration;

namespace SFA.DAS.Events.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ILog Logger = new NLogLogger();

        protected void Application_Start()
        {
            Logger.Info("Starting Events Api Application");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            TelemetryConfiguration.Active.InstrumentationKey = ConfigurationManager.AppSettings["APPINSIGHTS_INSTRUMENTATIONKEY"];
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
