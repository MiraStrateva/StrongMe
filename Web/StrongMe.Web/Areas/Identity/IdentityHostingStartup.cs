using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(StrongMe.Web.Areas.Identity.IdentityHostingStartup))]

namespace StrongMe.Web.Areas.Identity
{
    using Microsoft.AspNetCore.Hosting;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
