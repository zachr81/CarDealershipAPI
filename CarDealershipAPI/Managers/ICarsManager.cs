using CarDealershipAPI.Models;
using System.Collections.Generic;

namespace CarDealershipAPI.Managers
{
    public interface ICarsManager
    {
        /// <summary>
        /// Retrieve all available cars with full details
        /// </summary>
        /// <returns>List of cars</returns>
        IEnumerable<Car> GetCars();

        /// <summary>
        /// Retrieve a filtered list of cars
        /// </summary>
        /// <param name="carQueryFilters">Filters to apply to the list of cars</param>
        /// <param name="matchAll">If all given filters must match a car for it to be returned instead of only one.</param>
        /// <returns>List of cars that meets the given requirements</returns>
        IEnumerable<Car> SearchCars(List<CarFilter> filters, bool matchAll);
    }
}
