using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ITUniver.TeleCloud.WebCloud.Startup))]
namespace ITUniver.TeleCloud.WebCloud
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
