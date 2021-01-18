using CarDealershipAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;

        public CarsController(ILogger<CarsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Car> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Car
            {
                Make = Make.Chevy,
                price = rng.Next(5000, 20000),
                year = rng.Next(2010, 2021)
            })
            .ToArray();
        }

        [HttpPost]
        public IEnumerable<Car> SearchCars(CarQueryFilters carQueryFilters)
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Car
            {
                Make = Make.Chevy,
                price = rng.Next(5000, 20000),
                year = rng.Next(2010, 2021)
            })
            .ToArray();
        }
    }
}
