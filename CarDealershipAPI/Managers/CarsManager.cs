using CarDealershipAPI.Models;
using CarDealershipAPI.Providers;
using System;
using System.Collections.Generic;

namespace CarDealershipAPI.Managers
{
    public class CarsManager : ICarsManager
    {
        ICarsProvider carsProvider;

        public CarsManager(ICarsProvider carProvider)
        {
            carsProvider = carProvider;
        }

        /// <inheritdoc/>
        public IEnumerable<Car> GetCars()
        {
            return this.carsProvider.GetCars();
        }

        /// <inheritdoc/>
        public IEnumerable<Car> SearchCars(List<CarFilter> filters, bool matchAll)
        {
            var baseProperties = carsProvider.GetCars(filters, matchAll);
            var returnList = new List<Car>();
            foreach (var car in baseProperties)
            {
                // only return features that match the search along with make and year
                var returnCar = new Car
                {
                    Year = car.Year,
                    Make = car.Make,
                };
                foreach (var filter in filters)
                {
                    Enum.TryParse(filter.fieldValue, out Color color);
                    if (matchAll
                        || (filter.fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase) && LocalCarsProvider.GetPropertyValue<bool>(car, filter.fieldName) == bool.Parse(filter.fieldValue))
                        || (filter.fieldType.Equals("color", StringComparison.InvariantCultureIgnoreCase) && LocalCarsProvider.GetPropertyValue<Color>(car, filter.fieldName).Equals(color)))
                    {
                        if (filter.fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
                        {
                            LocalCarsProvider.SetPropertyValue<bool>(returnCar, filter.fieldName, bool.Parse(filter.fieldValue));
                        }
                        else if (filter.fieldType.Equals("color", StringComparison.InvariantCultureIgnoreCase))
                        {
                            LocalCarsProvider.SetPropertyValue<Color>(returnCar, filter.fieldName, color);
                        }
                        
                    }
                }
                returnList.Add(returnCar);
            }

            return returnList;
        }
    }
}
