using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkFlowTracker.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]     //Added this to load log4net configs from web.config
namespace WorkFlowTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
