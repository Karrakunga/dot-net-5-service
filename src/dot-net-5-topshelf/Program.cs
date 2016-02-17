using System.Linq;
using Microsoft.Extensions.PlatformAbstractions;
using Topshelf;

namespace dot_net_5_topshelf
{
    public class Program
    {
        private const string SERVICE_NAME = "dot-net-5-topshelf";

        private IApplicationEnvironment _applicationEnvironment;
        
        public Program(IApplicationEnvironment applicationEnvironment)
        {
            _applicationEnvironment = applicationEnvironment;
        }

        public void Main(string[] args)
        {
            args = args.Where(a => a != "<service-name>").ToArray();
            
            HostFactory.Run(x =>
            {
                x.ApplyCommandLine(string.Join(" ", args));

                x.Service<ServiceHost>(s =>
                {
                    s.ConstructUsing(name => new ServiceHost(_applicationEnvironment));
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                x.RunAsLocalSystem();
                x.SetDescription(SERVICE_NAME);
                x.SetDisplayName(SERVICE_NAME);
                x.SetServiceName(SERVICE_NAME);
            });
        }
    }
}
