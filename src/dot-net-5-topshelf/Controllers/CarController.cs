using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net_5_service.Models;
using Microsoft.AspNet.Mvc;

namespace dot_net_5_service.Controllers
{
    public class CarController : Controller
    {
        [Route("Car/Index")]
        public IActionResult Index()
        {
            var model = new CarModel {Type = "Jaguar"};
            return View(model);
        }
    }
}
