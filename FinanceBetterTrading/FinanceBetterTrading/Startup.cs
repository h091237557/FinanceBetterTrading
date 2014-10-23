using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinanceBetterTrading.Startup))]
namespace FinanceBetterTrading
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
