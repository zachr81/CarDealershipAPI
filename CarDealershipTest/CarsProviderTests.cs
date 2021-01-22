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
        ICarsProvider provider;

        public CarsProviderTests()
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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
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
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            provider = new LocalCarsProvider(sampleList);
        }

        [Fact]
        public void TestGetCarsEmpty()
        {
            var sampleList = new List<Car>
            {
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Empty(resultCars);
        }

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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
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
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Single(resultCars);
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
        }

        [Fact]
        public void TestGetCarsSingleMatch()
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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Single(resultCars);
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
        }

        [Fact]
        public void TestGetCarsSingleNoMatch()
        {
            var sampleList = new List<Car>
            {
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
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Empty(resultCars);
        }

        [Fact]
        public void TestGetCarsHeated()
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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                },
                new Car
                {
                    HasLowMiles = false,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasHeatedSeats",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Single(resultCars);
            Assert.All(resultCars, car => Assert.True(car.HasHeatedSeats));
        }

        [Fact]
        public void TestGetCarsColor()
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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                },
                new Car
                {
                    HasLowMiles = false,
                    HasHeatedSeats = false,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Red,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "Color",
                    fieldValue = "Red",
                    fieldType = "color"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Single(resultCars);
            Assert.All(resultCars, car => Assert.Equal(Color.Red, car.Color));
        }

        [Fact]
        public void TestGetCarsMultipleAll()
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
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                },
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                },
                new Car
                {
                    HasLowMiles = false,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "125"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                },
                new CarFilter
                {
                    fieldName = "Color",
                    fieldValue = "Black",
                    fieldType = "color"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, true);
            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
            Assert.All(resultCars, car => Assert.Equal(Color.Black, car.Color));
        }

        [Fact]
        public void TestGetCarsMultipleAny()
        {
            var sampleList = new List<Car>
            {
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = false,
                    HasNavigation = true,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                },
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                },
                new Car
                {
                    HasLowMiles = false,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = false,
                    HasSunroof = true,
                    Color = Color.Black,
                    IsFourWheelDrive = false,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "125"
                }
            };
            var provider = new LocalCarsProvider(sampleList);
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasNavigation",
                    fieldValue = "true",
                    fieldType = "boolean"
                },
                new CarFilter
                {
                    fieldName = "HasSunroof",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = provider.GetCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
            Assert.All(resultCars, car => Assert.True(car.HasNavigation || car.HasSunroof));
        }
    }
}
