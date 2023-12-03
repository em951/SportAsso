using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SportAssovv.StartupOwin))]

namespace SportAssovv
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
