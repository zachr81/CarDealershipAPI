using CarDealershipAPI;
using CarDealershipAPI.Models;
using CarDealershipAPI.Providers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using CarDealershipAPI.Managers;

namespace CarDealershipTest
{
    public class CarsManagerTests
    {
        ICarsManager manager;

        public CarsManagerTests()
        {
            var sampleList = new List<Car>
            {
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = true,
                    HasNavigation = false,
                    HasPowerWindows = true,
                    HasSunroof = true,
                    Color = Color.Black,
                    IsFourWheelDrive = true,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "123"
                },
                new Car
                {
                    HasLowMiles = true,
                    HasHeatedSeats = true,
                    HasNavigation = true,
                    HasPowerWindows = true,
                    HasSunroof = false,
                    Color = Color.Black,
                    IsFourWheelDrive = true,
                    Make = Make.Chevy,
                    Price = 10000,
                    Year = 2020,
                    Id = "124"
                }
            };
            var _mockProvider = new Mock<ICarsProvider>();
            _mockProvider.Setup(provider => provider.GetCars(It.IsAny<List<CarFilter>>(), It.IsAny<bool>())).Returns(sampleList);
            manager = new CarsManager(_mockProvider.Object);
        }

        [Fact]
        public void TestGetFilterEmpty()
        {
            var sampleQuery = new List<CarFilter>
            {
            };
            var resultCars = manager.SearchCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
            Assert.All(resultCars, car => Assert.False(car.IsFourWheelDrive));
            Assert.All(resultCars, car => Assert.False(car.HasPowerWindows));
            Assert.All(resultCars, car => Assert.Equal(Color.None, car.Color));
            Assert.All(resultCars, car => Assert.NotEqual(Make.None, car.Make));
            Assert.All(resultCars, car => Assert.NotEqual(0, car.Year));
        }

        [Fact]
        public void TestGetFilterSingleBool()
        {
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = manager.SearchCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
            Assert.All(resultCars, car => Assert.False(car.IsFourWheelDrive));
            Assert.All(resultCars, car => Assert.False(car.HasPowerWindows));
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
            Assert.All(resultCars, car => Assert.Equal(Color.None, car.Color));
            Assert.All(resultCars, car => Assert.NotEqual(Make.None, car.Make));
            Assert.All(resultCars, car => Assert.NotEqual(0, car.Year));
        }

        [Fact]
        public void TestGetFilterSingleColor()
        {
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "Color",
                    fieldValue = "Black",
                    fieldType = "color"
                }
            };
            var resultCars = manager.SearchCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
            Assert.All(resultCars, car => Assert.False(car.IsFourWheelDrive));
            Assert.All(resultCars, car => Assert.False(car.HasPowerWindows));
            Assert.All(resultCars, car => Assert.Equal(Color.Black, car.Color));
            Assert.All(resultCars, car => Assert.NotEqual(Make.None, car.Make));
            Assert.All(resultCars, car => Assert.NotEqual(0, car.Year));
        }

        [Fact]
        public void TestGetFilterColorAndBool()
        {
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "Color",
                    fieldValue = "Black",
                    fieldType = "color"
                },
                new CarFilter
                {
                    fieldName = "HasLowMiles",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = manager.SearchCars(sampleQuery, true);
            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
            Assert.All(resultCars, car => Assert.False(car.IsFourWheelDrive));
            Assert.All(resultCars, car => Assert.False(car.HasPowerWindows));
            Assert.All(resultCars, car => Assert.True(car.HasLowMiles));
            Assert.All(resultCars, car => Assert.Equal(Color.Black, car.Color));
            Assert.All(resultCars, car => Assert.NotEqual(Make.None, car.Make));
            Assert.All(resultCars, car => Assert.NotEqual(0, car.Year));
        }

        [Fact]
        public void TestGetAnyFilterPartialMatch()
        {
            var sampleQuery = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "HasSunroof",
                    fieldValue = "true",
                    fieldType = "boolean"
                },
                new CarFilter
                {
                    fieldName = "HasNavigation",
                    fieldValue = "true",
                    fieldType = "boolean"
                }
            };
            var resultCars = manager.SearchCars(sampleQuery, false);
            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
            Assert.All(resultCars, car => Assert.False(car.IsFourWheelDrive));
            Assert.All(resultCars, car => Assert.False(car.HasPowerWindows));
            Assert.All(resultCars, car => Assert.True(car.HasNavigation || car.HasSunroof));
            Assert.Single(resultCars.Where(car => car.HasNavigation));
            Assert.Single(resultCars.Where(car => car.HasSunroof));
            Assert.All(resultCars, car => Assert.Equal(Color.None, car.Color));
            Assert.All(resultCars, car => Assert.NotEqual(Make.None, car.Make));
            Assert.All(resultCars, car => Assert.NotEqual(0, car.Year));
        }
    }
}
