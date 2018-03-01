using System;
using System.Linq;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Eatech.FleetManager.ApplicationCore.Services;
using Xunit;

namespace Eatech.FleetManager.UnitTests.ApplicationCore.Services.CarServiceTests
{
    public class CarGet
    {
        [Fact]
        public async void AllCars()
        {
            ICarService carService = new CarService();

            var cars = (await carService.GetAll()).ToList();

            Assert.NotNull(cars);
            Assert.NotEmpty(cars);
            Assert.Equal(2, cars.Count);
        }

        [Fact]
        public async void ExistingCardWithId()
        {
            ICarService carService = new CarService();
            var carId = Guid.Parse("d9417f10-5c79-44a0-9137-4eba914a82a9");

            var car = await carService.Get(carId);

            Assert.NotNull(car);
            Assert.Equal(carId, car.Id);
        }

        [Fact]
        public async void NonExistingCardWithId()
        {
            ICarService carService = new CarService();
            var carId = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");

            var car = await carService.Get(carId);

            Assert.Null(car);
        }
    }
}