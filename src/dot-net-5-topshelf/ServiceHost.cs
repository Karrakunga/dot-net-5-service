using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace dot_net_5_topshelf
{
    public class ServiceHost
    {
        private IApplication _application;
        private readonly IApplicationEnvironment _applicationEnvironment;

        public ServiceHost(IApplicationEnvironment applicationEnvironment)
        {
            _applicationEnvironment = applicationEnvironment;
        }

        public void Start()
        {
            var configSource = new JsonConfigurationProvider($@"{_applicationEnvironment.ApplicationBasePath}\config.json");
            
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
