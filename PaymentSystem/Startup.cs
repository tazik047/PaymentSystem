using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaymentSystem.Startup))]
namespace PaymentSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
