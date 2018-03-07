using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;

namespace Eatech.FleetManager.ApplicationCore.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAll();

        Task<Car> Get(Guid id);

        Task<Car> Add(Car car);

        Task<Car> Update(
            Guid Id, 
            int? ModelYear = null, 
            string Model = null, 
            string Manufacturer = null, 
            string Registration = null, 
            DateTime? InspectionDate = null, 
            float? EngineSize = null, 
            float? EnginePower = null);

        Task<Car> Remove(Guid Id);
    }
}