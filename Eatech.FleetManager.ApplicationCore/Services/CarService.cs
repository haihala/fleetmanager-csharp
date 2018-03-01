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
        private static readonly ImmutableList<Car> TempCars = new List<Car>
        {
            new Car
            {
                Id = Guid.Parse("d9417f10-5c79-44a0-9137-4eba914a82a9"),
                ModelYear = 1998
            },
            new Car
            {
                Id = Guid.NewGuid(),
                ModelYear = 2007
            }
        }.ToImmutableList();

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await Task.FromResult(TempCars);
        }

        public async Task<Car> Get(Guid id)
        {
            return await Task.FromResult(TempCars.FirstOrDefault(c => c.Id == id));
        }
    }
}