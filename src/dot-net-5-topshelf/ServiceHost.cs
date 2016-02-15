using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace dot_net_5_topshelf
{
    public class ServiceHost
    {
        private IApplication _application;

        public void Start()
        {
            var configSource = new MemoryConfigurationProvider { {"server.urls", "http://localhost:5000"} };

            var config = new ConfigurationBuilder()
                .Add(configSource)
                .Build();

            var builder = new WebHostBuilder(config);
            builder.UseServer("Microsoft.AspNet.Server.Kestrel");
            builder.UseServices(services => services.AddMvc());
            builder.UseStartup(appBuilder =>
            {
                appBuilder.UseDefaultFiles();
                appBuilder.UseStaticFiles();
                appBuilder.UseMvc();
            });

            _application = builder.Build().Start();
        }

        public void Stop()
        {
            _application?.Dispose();
        }
    }
}
