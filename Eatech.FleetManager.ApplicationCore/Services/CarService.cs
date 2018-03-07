using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;

namespace Eatech.FleetManager.ApplicationCore.Services
{
    public class CarService : ICarService
    {
        SqliteConnection DBConnection;
        public CarService ()
        {
            DBConnection = new SqliteConnection("Data Source=../Eatech.FleetManager.ApplicationCore/database.sqlite;");
            DBConnection.Open();
        }


        public async Task<IEnumerable<Car>> GetAll()
        {
            SqliteCommand command = new SqliteCommand("SELECT * FROM cars", DBConnection);
            SqliteDataReader reader = await command.ExecuteReaderAsync();
            return CarsFromQuery(reader);
        }

        public async Task<Car> Get(Guid id)
        {
            SqliteCommand command = new SqliteCommand(String.Format("SELECT * FROM cars WHERE Id = '{0}'", id), DBConnection);
            SqliteDataReader reader = await command.ExecuteReaderAsync();
            List<Car> result = CarsFromQuery(reader);
            if (result.Count > 0)
            {
                return result[0];
            }

            return null;
        }

        public async Task<Car> Add(Car car)
        {
            string query = String.Format(
                "INSERT INTO cars " +
                "(Id, ModelYear, Model, Manufacturer, Registration, InspectionDate, EngineSize, EnginePower) " +
                "values " +
                "('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                car.Id.ToString(),
                car.ModelYear,
                car.Model,
                car.Manufacturer,
                car.Registration,
                car.InspectionDate.ToString(),
                car.EngineSize,
                car.EnginePower
                );

            SqliteCommand command = new SqliteCommand(query, DBConnection);
            command.ExecuteNonQuery();
            
            return await Get(car.Id);
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

        public async Task<Car> Remove(Guid id)
        {
            Car car = await Get(id);

            SqliteCommand command = new SqliteCommand(String.Format("DELETE FROM cars WHERE Id = '{0}'", id), DBConnection);
            command.ExecuteNonQuery();
            
            return car;
        }

    
        private List<Car> CarsFromQuery(SqliteDataReader reader)
        {
            List<Car> cars = new List<Car>();
            while (reader.Read())
            {
                cars.Add(new Car
                {
                    Id = Guid.Parse(reader["Id"].ToString()),
                    ModelYear = int.TryParse(reader["ModelYear"].ToString(), out int year) ? (int?)year : null,
                    Model = reader["Model"]?.ToString(),
                    Manufacturer = reader["Manufacturer"]?.ToString(),
                    Registration = reader["Registration"]?.ToString(),
                    InspectionDate = DateTime.TryParse(reader["InspectionDate"].ToString(), out DateTime inspectionDate) ? (DateTime?)inspectionDate : null,
                    EngineSize = float.TryParse(reader["EngineSize"].ToString(), out float engineSize) ? (float?)engineSize : null,
                    EnginePower = float.TryParse(reader["EnginePower"].ToString(), out float EnginePower) ? (float?)EnginePower : null
                });
            }
        return cars;
        }
    }
}