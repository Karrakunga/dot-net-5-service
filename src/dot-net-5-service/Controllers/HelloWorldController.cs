using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace dot_net_5_service.Controllers
{
    public class HelloWorldController : Controller
    {
        [Route("DateTime")]
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }
}
