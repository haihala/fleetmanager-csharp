using System;
using System.Linq;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Eatech.FleetManager.ApplicationCore.Services;
using Xunit;
using Eatech.FleetManager.ApplicationCore.Entities;

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
        }

        [Fact]
        public async void FilteredAllCars()
        {
            ICarService carService = new CarService();

            var cars = (await carService.GetAll(
                minYear: 1000,
                maxYear: 3000,
                model: "model7",
                manufacturer: "make10")).ToList();

            Assert.NotNull(cars);
            Assert.NotEmpty(cars);
        }

        [Fact]
        public async void ExistingCardWithId()
        {
            ICarService carService = new CarService();
            var carId = Guid.Parse("231dcb10-d3ee-4687-aa4a-25f507dc340b");

            var car = await carService.Get(carId);

            Assert.NotNull(car);
            Assert.Equal(carId, car.Id);
        }

        [Fact]
        public async void NonExistingCardWithId()
        {
            ICarService carService = new CarService();
            Guid carId = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");

            Car car = await carService.Get(carId);

            Assert.Null(car);
        }
    }
}