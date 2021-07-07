// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Configuration;
using System.Web;
using FluentValidation;
using MediatR;
using SFA.DAS.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.Events.Domain.Logging;
using SFA.DAS.Events.Domain.Repositories;
using SFA.DAS.Events.Infrastructure.Configuration;
using SFA.DAS.Events.Infrastructure.Data;
using SFA.DAS.Events.Infrastructure.Logging;
using SFA.DAS.NLog.Logger;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace SFA.DAS.Events.Api.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        private const string ServiceName = "SFA.DAS.Events";
        private const string Version = "1.0";

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory(a => a.GetName().Name.StartsWith(ServiceName));
                    scan.RegisterConcreteTypesAgainstTheFirstInterface();
                    scan.ConnectImplementationsToTypesClosing(typeof(AbstractValidator<>));
                });

            var config = GetConfiguration();

            For<IApprenticeshipEventRepository>().Use<ApprenticeshipEventRepository>().Ctor<string>().Is(config.DatabaseConnectionString);
            For<IAgreementEventRepository>().Use<AgreementEventRepository>().Ctor<string>().Is(config.DatabaseConnectionString);
            For<IAccountEventRepository>().Use<AccountEventRepository>().Ctor<string>().Is(config.DatabaseConnectionString);
            For<IGenericEventRepository>().Use<GenericEventRepository>().Ctor<string>().Is(config.DatabaseConnectionString);


            RegisterMediator();

            ConfigureLogging();
        }

        private void ConfigureLogging()
        {
            For<ILoggingContext>().Use(x => new WebLoggingContext(new HttpContextWrapper(HttpContext.Current)));
            For<ILog>().Use(c => new NLogLogger(c.ParentType, c.GetInstance<ILoggingContext>(), null)).AlwaysUnique();
            For<IEventsLogger>().Use(x => GetBaseLogger(x)).AlwaysUnique();
        }

        private IEventsLogger GetBaseLogger(IContext x)
        {
            return new EventsLogger(x.GetInstance<ILog>());
        }

        private EventConfiguration GetConfiguration()
        {
            var environment = ConfigurationManager.AppSettings["EnvironmentName"];

            var configurationRepository = GetConfigurationRepository();
            var configurationService = new ConfigurationService(configurationRepository, new ConfigurationOptions(ServiceName, environment, Version));

            return configurationService.Get<EventConfiguration>();
        }

        private static IConfigurationRepository GetConfigurationRepository()
        {
            return new AzureTableStorageConfigurationRepository(ConfigurationManager.AppSettings["ConfigurationStorageConnectionString"]);
        }

        private void RegisterMediator()
        {
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For<IMediator>().Use<Mediator>();
        }
    }
}
