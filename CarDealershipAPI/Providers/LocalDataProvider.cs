using CarDealershipAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarDealershipAPI.Providers
{
    public class LocalDataProvider
    {
        private IEnumerable<Car> storedCars;
        public LocalDataProvider()
        {
            storedCars = JsonSerializer.Deserialize<ISet<Car>>(File.ReadAllText(@".\Cars.json"));
        }

        public LocalDataProvider(List<Car> overrideCars)
        {
            storedCars = overrideCars;
        }

        public IEnumerable<Car> GetCars()
        {
            return storedCars;
        }

        public IEnumerable<Car> GetCars(CarQueryFilters filters)
        {
            IEnumerable<Car> returnList;
            var properties = filters.GetType().GetProperties();
            if (filters.matchAllFilters)
            {
                returnList = storedCars;
                foreach (var property in properties)
                {
                    // need to ignore properties that were not set in the request
                    if (property.PropertyType == typeof(bool) && (bool)property.GetValue(filters) != false)
                    {
                        returnList = returnList.Where(car => (bool)property.GetValue(car) == (bool)property.GetValue(filters));
                    }
                    else if (property.PropertyType == typeof(Color) && (Color)property.GetValue(filters) != Color.None)
                    {
                        returnList = returnList.Where(car => (Color)property.GetValue(car) == (Color)property.GetValue(filters));
                    }
                }
            }
            else
            {
                returnList = new HashSet<Car>();
                foreach (var property in properties)
                {
                    // need to ignore properties that were not set in the request
                    if (property.PropertyType == typeof(bool) && (bool)property.GetValue(filters) != false)
                    {
                        var filteredCars = storedCars.Where(car => (bool)property.GetValue(car) == (bool)property.GetValue(filters));
                        returnList = returnList.Union(filteredCars);
                    }
                    else if (property.PropertyType == typeof(Color) && (Color)property.GetValue(filters) != Color.None)
                    {
                        var filteredCars = storedCars.Where(car => (Color)property.GetValue(car) == (Color)property.GetValue(filters));
                        returnList = returnList.Union(filteredCars);
                    }
                }
            }

            return returnList;
        }
    }
}
