using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SFA.DAS.Events.Api.Startup))]

namespace SFA.DAS.Events.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
