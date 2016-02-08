using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dot_net_5_service.Models;
using Microsoft.AspNet.Mvc;

namespace dot_net_5_service.Controllers
{
    public class CarJsonController : Controller
    {
        [Route("Car")]
        public IEnumerable<CarModel> Get()
        {
            var cars = new List<CarModel>
            {
                new CarModel {Type = "Jaguar"},
                new CarModel {Type = "Ford"}
            };

            return cars;
        }
    }
}
