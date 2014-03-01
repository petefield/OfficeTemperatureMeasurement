using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Temperature_Display.Startup))]
namespace Temperature_Display
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
            
        }
    }
}
