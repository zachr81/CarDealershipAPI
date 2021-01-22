using CarDealershipAPI.Controllers;
using CarDealershipAPI.Managers;
using CarDealershipAPI.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace CarDealershipTest
{
    public class CarsControllerTests
    {
        CarsController controller;

        public CarsControllerTests()
        {
            var _mockLogger = new Mock<ILogger<CarsController>>();
            var _mockManager = new Mock<ICarsManager>();
            var testData = new List<Car>
            {
                new Car
                {
                    Color = CarDealershipAPI.Color.Black,
                    HasHeatedSeats = true,
                    HasLowMiles = true,
                    HasNavigation = true,
                    Make = CarDealershipAPI.Make.Chevy,
                    Year = 2000,
                    HasPowerWindows = true,
                    HasSunroof = true,
                    IsFourWheelDrive = true,
                    Price = 20000,
                    Id = "1"
                },
                new Car
                {
                    Color = CarDealershipAPI.Color.Red,
                    HasHeatedSeats = false,
                    HasLowMiles = false,
                    HasNavigation = false,
                    Make = CarDealershipAPI.Make.Mercedes,
                    Year = 2010,
                    HasPowerWindows = false,
                    HasSunroof = false,
                    IsFourWheelDrive = true,
                    Price = 10000,
                    Id = "2"
                }
            };
            _mockManager.Setup(manager => manager.SearchCars(It.IsAny<List<CarFilter>>(), It.IsAny<bool>())).Returns(testData);
            _mockManager.Setup(manager => manager.GetCars()).Returns(testData);
            controller = new CarsController(_mockLogger.Object, _mockManager.Object);
        }

        [Fact]
        public void TestGetCarsEmptyQuery()
        {
            var resultCars = controller.Get();

            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
        }

        [Fact]
        public void TestGetCarsNoFilters()
        {
            var resultCars = controller.SearchCars(new List<CarFilter>(), true);

            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
        }

        [Fact]
        public void TestInvalidFilterType()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive",
                    fieldType = "test",
                    fieldValue = "true"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Only bool and color filter types currently supported", ex.errorMessage);
        }

        [Fact]
        public void TestUnusualCapsFilterType()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive",
                    fieldType = "bOoLEaN",
                    fieldValue = "true"
                }
            };
            var resultCars = controller.SearchCars(filterList, true);

            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
        }

        [Fact]
        public void TestInvalidFilterName()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive2",
                    fieldType = "boolean",
                    fieldValue = "true"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Submitted filter does not match available properties, check input and try again", ex.errorMessage);
        }

        [Fact]
        public void TestUnusualCapsFilterName()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "isFOuRwhEEldrIve",
                    fieldType = "boolean",
                    fieldValue = "true"
                }
            };
            var resultCars = controller.SearchCars(filterList, true);

            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
        }

        [Fact]
        public void TestInvalidBoolFilterValue()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive",
                    fieldType = "boolean",
                    fieldValue = "not"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Submitted value for bool type must be true or false", ex.errorMessage);
        }

        [Fact]
        public void TestUnusualCapsFilterValue()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive",
                    fieldType = "boolean",
                    fieldValue = "TrUe"
                }
            };
            var resultCars = controller.SearchCars(filterList, true);

            Assert.NotNull(resultCars);
            Assert.Equal(2, resultCars.Count());
        }

        [Fact]
        public void TestInvalidColorFilterValue()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "Color",
                    fieldType = "color",
                    fieldValue = "turquoise"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Invalid color given, please choose one of the available colors", ex.errorMessage);
        }

        [Fact]
        public void TestMismatchColorFilterType()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "IsFourWheelDrive",
                    fieldType = "color",
                    fieldValue = "Red"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Given property name and type do not match, check values and try again", ex.errorMessage);
        }

        [Fact]
        public void TestMismatchBoolFilterType()
        {
            var filterList = new List<CarFilter>
            {
                new CarFilter
                {
                    fieldName = "Color",
                    fieldType = "boolean",
                    fieldValue = "true"
                }
            };
            var ex = Assert.Throws<CarException>(() => controller.SearchCars(filterList, true));
            Assert.Equal("Given property name and type do not match, check values and try again", ex.errorMessage);
        }
    }
}
