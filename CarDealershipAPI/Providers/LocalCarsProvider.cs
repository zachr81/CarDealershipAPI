using CarDealershipAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CarDealershipAPI.Providers
{
    public class LocalCarsProvider : ICarsProvider
    {
        private IEnumerable<Car> storedCars;
        public LocalCarsProvider()
        {
            storedCars = JsonConvert.DeserializeObject<List<Car>>(File.ReadAllText(@".\Cars.json"));
        }

        public LocalCarsProvider(List<Car> overrideCars)
        {
            storedCars = overrideCars;
        }

        public IEnumerable<Car> GetCars()
        {
            return storedCars;
        }

        public IEnumerable<Car> GetCars(List<CarFilter> filters, bool matchAll)
        {
            IEnumerable<Car> returnList;
            if (matchAll)
            {
                returnList = storedCars;
                foreach (var filter in filters)
                {
                    if (filter.fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
                    {
                        bool.TryParse(filter.fieldValue, out var boolValue);
                        returnList = returnList.Where(car => LocalCarsProvider.GetPropertyValue<bool>(car, filter.fieldName) == boolValue);
                    }
                    else if (filter.fieldType.Equals("color", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Enum.TryParse(filter.fieldValue, out Color color);
                        returnList = returnList.Where(car => LocalCarsProvider.GetPropertyValue<Color>(car, filter.fieldName).Equals(color));
                    }
                }
            }
            else
            {
                returnList = new HashSet<Car>();
                foreach (var filter in filters)
                {
                    if (filter.fieldType.Equals("boolean", StringComparison.InvariantCultureIgnoreCase))
                    {
                        bool.TryParse(filter.fieldValue, out var boolValue);
                        var filteredCars = storedCars.Where(car => LocalCarsProvider.GetPropertyValue<bool>(car, filter.fieldName) == boolValue);
                        returnList = returnList.Union(filteredCars);
                    }
                    else if (filter.fieldType.Equals("color", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Enum.TryParse(filter.fieldValue, out Color color);
                        var filteredCars = storedCars.Where(car => LocalCarsProvider.GetPropertyValue<Color>(car, filter.fieldName).Equals(color));
                        returnList = returnList.Union(filteredCars);
                    }
                }
            }

            return returnList;
        }

        /// <summary>
        /// Get the value of the property with the given name for the given object
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="obj">Object to read from</param>
        /// <param name="propName">Name of the property</param>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object obj, string propName)
        {
            return (T)obj.GetType().GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(obj, null);
        }

        /// <summary>
        /// Change the value of a given property on the object
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="obj">Object to update</param>
        /// <param name="propName">Name of the property</param>
        /// <param name="value">Value to set</param>
        public static void SetPropertyValue<T>(object obj, string propName, T value)
        {
            obj.GetType().GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, value);
        }
    }
}
