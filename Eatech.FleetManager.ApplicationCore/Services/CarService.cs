using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;

namespace Eatech.FleetManager.ApplicationCore.Services
{
    public class CarService : ICarService
    {
        /// <summary>
        ///     Remove this. Temporary car storage before proper data storage is implemented.
        /// </summary>
        private static readonly List<Car> TempCars = new List<Car>
        {
            new Car
            {
                Id = Guid.Parse("d9417f10-5c79-44a0-9137-4eba914a82a9"),
                ModelYear = 1998,
                Model = "escort",
                Manufacturer = "Ford"
            },
            new Car
            {
                Id = Guid.NewGuid(),
                ModelYear = 2007,
                Model = "yaris",
                Manufacturer = "Toyota"
            }
        };

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await Task.FromResult(TempCars);
        }

        public async Task<Car> Get(Guid id)
        {
            return await Task.FromResult(TempCars.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Car> Add(Guid Id, int ModelYear, string Model, string Manufacturer)
        {
            Car car = new Car
            {
                Id = Id,
                ModelYear = ModelYear,
                Model = Model,
                Manufacturer = Manufacturer
            };

            TempCars.Add(car);
            return await Task.FromResult(car);
        }

        public async Task<Car> Add(Car car)
        {
            return await Add(car.Id, car.ModelYear, car.Model, car.Manufacturer);
        }

        public async Task<Car> Update(Guid Id, int? ModelYear = null, string Model = null, string Manufacturer = null)
        {
            Car car = await Remove(Id);
            if (ModelYear != null)
            {
                car.ModelYear = (int) ModelYear;
            }

            if (Model != null)
            {
                car.Model = Model;
            }

            if (Manufacturer != null)
            {
                car.Manufacturer = Manufacturer;
            }
            return await Add(car);
        }

        public async Task<Car> Remove(Guid Id)
        {
            Car car = Get(Id).Result;
            TempCars.Remove(car);
            return await Task.FromResult(car);
        }

    }
}