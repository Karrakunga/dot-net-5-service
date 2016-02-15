using System;
using System.ServiceProcess;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;


namespace dot_net_5_service
{
    public class Program : ServiceBase
    {
        private IApplication _application;
        private readonly IApplicationEnvironment _applicationEnvironment;

        public Program(IApplicationEnvironment applicationEnvironment)
        {
            _applicationEnvironment = applicationEnvironment;
        }

        public void Main(string[] args)
        {
            ConfigHandler.Initialise(_applicationEnvironment);
            Console.WriteLine($"Service listenting on {ConfigHandler.Configuration["server.urls"]}");

            if (Environment.UserInteractive)
            {
                OnStart(null);
                Console.ReadLine();
                OnStop();
            }
            else
            {
                Run(this);
            }
        }

        protected override void OnStart(string[] args)
        {
            //var configSource = new JsonConfigurationProvider("config.json");
            //var configSource = new MemoryConfigurationProvider{ {"server.urls", "http://localhost:5000"} };

            //var config = new ConfigurationBuilder()
            //    .Add(configSource)
            //    .Build();

            var builder = new WebHostBuilder(ConfigHandler.Configuration);
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

        protected override void OnStop()
        {
            _application?.Dispose();
        }
    }
}
