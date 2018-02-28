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

        public async Task<IEnumerable<Car>> GetAll(Dictionary<string, string> search)
        {
            return await Task.FromResult(TempCars);
        }

        public async Task<Car> Get(Guid id)
        {
            return await Task.FromResult(TempCars.FirstOrDefault(c => c.Id == id));
        }

        public async Task<Car> Add(Car car)
        {
            TempCars.Add(car);
            return await Task.FromResult(car);
        }

        public async Task<Car> Update(
            Guid Id, 
            int? ModelYear = null, 
            string Model = null, 
            string Manufacturer = null, 
            string Registration = null, 
            DateTime? InspectionDate = null, 
            float? EngineSize = null, 
            float? EnginePower = null)
        {
            Car car = await Remove(Id) ?? new Car();

            car.Id = Id;
            car.ModelYear = ModelYear ?? car.ModelYear;
            car.Model = Model ?? car.Model;
            car.Manufacturer = Manufacturer ?? car.Manufacturer;
            car.Registration = Registration ?? car.Registration;
            car.InspectionDate = InspectionDate ?? car.InspectionDate;
            car.EngineSize = EngineSize ?? car.EngineSize;
            car.EnginePower = EnginePower ?? car.EnginePower;

            return await Add(car);
        }

        public async Task<Car> Remove(Guid Id)
        {
            Car car = await Get(Id);
            if (car != null)
            {
                TempCars.Remove(car);
            }
            return await Task.FromResult(car);
            
        }

    }
}