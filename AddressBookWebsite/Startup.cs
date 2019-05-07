using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddressBookWebsite.Startup))]
namespace AddressBookWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
