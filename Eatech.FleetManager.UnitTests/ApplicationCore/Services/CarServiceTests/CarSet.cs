using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Eatech.FleetManager.ApplicationCore.Services;
using System;
using Xunit;

namespace Eatech.FleetManager.UnitTests.ApplicationCore.Services.CarServiceTests
{
    public class CarSet
    {
        [Fact]
        public async void AddRemoveCar()
        {
            // Adding and removing should be done in the same test, since that allows for tests that don't mutate the data.
            ICarService carService = new CarService();

            Guid id = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");
            bool existsBefore = await carService.Get(id) != null;
            await carService.Update(id);
            bool existsMiddle = await carService.Get(id) != null;
            await carService.Remove(id);
            bool existsAfter = await carService.Get(id) != null;

            Assert.False(existsBefore);
            Assert.True(existsMiddle);
            Assert.False(existsAfter);
        }

        [Fact]
        public async void AddRemoveEqualCar()
        {
            // Add a new car for testing and change it.
            ICarService carService = new CarService();

            Guid id = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");
            Car car1 = await carService.Update(id);
            Car car2 = await carService.Remove(id);
            
            Assert.True(car1.Equals(car2));
        }

        [Fact]
        public async void ChangeCar()
        {
            // Add a new car for testing and change it.
            ICarService carService = new CarService();

            Guid id = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");
            Car car1 = await carService.Update(id);
            Car car2 = await carService.Update(id, 
                modelYear: 1999);
            Car car3 = await carService.Remove(id);

            Assert.False(car1.Equals(car2));
            Assert.True(car2.Equals(car3));
        }
        [Fact]
        public async void FakeChangeCar()
        {
            // Change car to what it already is. Cars should equal.
            ICarService carService = new CarService();

            Guid id = Guid.Parse("d9417f10-1111-1111-1111-4eba914a82a9");
            Car car1 = await carService.Update(id,
                modelYear: 2000,
                model: "model",
                manufacturer: "maker",
                registration: "abc-123",
                inspectionDate: DateTime.Parse("2018-05-08T00:00:00"),
                engineSize: 100,
                enginePower: 42);
            Car car2 = await carService.Update(id,
                modelYear: 2000,
                model: "model",
                manufacturer: "maker",
                registration: "abc-123",
                inspectionDate: DateTime.Parse("2018-05-08T00:00:00"),
                engineSize: 100,
                enginePower: 42);
            Car car3 = await carService.Remove(id);

            Assert.True(car1.Equals(car2));
            Assert.True(car2.Equals(car3));
        }
    }


}
