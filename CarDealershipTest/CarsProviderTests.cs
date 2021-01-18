using CarDealershipAPI;
using CarDealershipAPI.Models;
using CarDealershipAPI.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace CarDealershipTest
{
    public class CarsProviderTests
    {
        [Fact]
        public void TestGetCarsMileage()
        {
            var sampleList = new List<Car>
            {
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = false,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    price = 10000,
                    year = 2020,
                    _id = "123"
                },
                new Car
                {
                    HasLowMiles = false,
                    HasHeatedSeats = false,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    price = 10000,
                    year = 2020,
                    _id = "124"
                }
            };
            var provider = new LocalDataProvider(sampleList);
            var sampleQuery = new CarQueryFilters
            {
                HasLowMiles = true
            };
            var resultCars = provider.GetCars(sampleQuery);
            Assert.NotNull(resultCars);
            Assert.Single(resultCars);
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
        }
    }
}
