using CarDealershipAPI.Models;
using System.Collections.Generic;

namespace CarDealershipAPI.Providers
{
    public interface ICarsProvider
    {
        /// <summary>
        /// Retrieve all available cars with full details
        /// </summary>
        /// <returns>List of cars</returns>
        public IEnumerable<Car> GetCars();

        /// <summary>
        /// Retrieve a filtered list of cars
        /// </summary>
        /// <param name="carQueryFilters">Filters to apply to the list of cars</param>
        /// <param name="matchAll">If all given filters must match a car for it to be returned instead of only one.</param>
        /// <returns>List of cars that meets the given requirements</returns>
        public IEnumerable<Car> GetCars(List<CarFilter> filters, bool matchAll);
    }
}
