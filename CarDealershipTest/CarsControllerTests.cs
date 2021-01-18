using CarDealershipAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace CarDealershipTest
{
    public class CarsControllerTests
    {
        [Fact]
        public void TestGetCarsEmptyQuery()
        {
            var _mockLogger = new Mock<ILogger<CarsController>>();
            var controller = new CarsController(_mockLogger.Object);
            var resultCars = controller.Get();

            Assert.NotNull(resultCars);
            Assert.NotEmpty(resultCars);
        }
    }
}
