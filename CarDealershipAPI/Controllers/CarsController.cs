using CarDealershipAPI.Managers;
using CarDealershipAPI.Models;
using CarDealershipAPI.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CarDealershipAPI.Controllers
{
    [ApiController]
    [Route("/cars")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly ICarsManager _carsManager;

        public CarsController(ILogger<CarsController> logger, ICarsManager carsManager)
        {
            _logger = logger;
            _carsManager = carsManager;
        }

        /// <summary>
        /// Retrieve all available cars with full details
        /// </summary>
        /// <returns>List of cars</returns>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return _carsManager.GetCars();
        }

        /// <summary>
        /// Retrieve a filtered list of cars
        /// </summary>
        /// <param name="carQueryFilters">Filters to apply to the list of cars</param>
        /// <param name="matchAll">If all given filters must match a car for it to be returned instead of only one.</param>
        /// <returns>List of cars that meets the given requirements</returns>
        [HttpPost]
        [Route("/cars/search")]
        public IEnumerable<Car> SearchCars([FromBody]List<CarFilter> carQueryFilters, bool matchAll)
        {
            foreach (var filter in carQueryFilters)
            {
                var filterField = typeof(Car).GetProperty(filter.fieldName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (filterField == null)
                {
                    throw new CarException("Submitted filter does not match available properties, check input and try again");
                }
                if (filter.fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
                {
                    var validBool = bool.TryParse(filter.fieldValue, out var boolValue);
                    if (!validBool)
                    {
                        throw new CarException("Submitted value for bool type must be true or false");
                    }
                }
                else if (filter.fieldType.Equals("color"))
                {
                    Enum.TryParse(filter.fieldValue, out Color color);
                    if (color.Equals(Color.None))
                    {
                        throw new CarException("Invalid color given, please choose one of the available colors");
                    }
                }
                else
                {
                    throw new CarException("Only bool and color filter types currently supported");
                }
                if (!filterField.PropertyType.Name.Equals(filter.fieldType, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new CarException("Given property name and type do not match, check values and try again");
                }
            }
            return _carsManager.SearchCars(carQueryFilters, matchAll);
        }

        /// <summary>
        /// Handle the error case
        /// </summary>
        /// <returns>Formatted error message</returns>
        [Route("/error")]
        public IActionResult Error() => Problem();
    }
}
