using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.PlatformAbstractions;

namespace dot_net_5_service
{
    public class ConfigHandler
    {
        public static void Initialise(IApplicationEnvironment applicationEnvironment)
        {
            var configProvider = new JsonConfigurationProvider($@"{applicationEnvironment.ApplicationBasePath}\config.json");
            Configuration = new ConfigurationBuilder().Add(configProvider).Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }
    }
}
