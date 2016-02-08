using System;
using System.ServiceProcess;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;


namespace dot_net_5_service
{
    public class Program : ServiceBase
    {
        private IApplication _application;

        public void Main(string[] args)
        {
            if (System.Diagnostics.Debugger.IsAttached)
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
            var configSource = new JsonConfigurationProvider("config.json");

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

        protected override void OnStop()
        {
            _application?.Dispose();
        }
    }
}
