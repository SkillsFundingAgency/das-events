﻿using System.Configuration;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using SFA.DAS.ApiTokens.Client;

namespace SFA.DAS.Events.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StrictEnumConverter());

            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof (IExceptionHandler), new ValidationExceptionHandler());
        }
    }
}
