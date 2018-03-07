using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;

namespace Eatech.FleetManager.ApplicationCore.Interfaces
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAll(
            int? minYear = null,
            int? maxYear = null,
            string model = null,
            string manufacturer = null);

        Task<Car> Get(Guid id);

        Task<Car> Add(Car car);

        Task<Car> Update(
            Guid id, 
            int? modelYear = null, 
            string model = null, 
            string manufacturer = null, 
            string registration = null, 
            DateTime? inspectionDate = null, 
            float? engineSize = null, 
            float? enginePower = null);

        Task<Car> Remove(Guid Id);
    }
}