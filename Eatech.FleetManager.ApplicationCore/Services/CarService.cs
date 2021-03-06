﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Microsoft.Data.Sqlite;
using System.IO;
using System.Linq;

namespace Eatech.FleetManager.ApplicationCore.Services
{
    public class CarService : ICarService
    {
        SqliteConnection DBConnection;
        public CarService ()
        {
            // Disclaimer on the following path mess.
            // Tests are ran from a different path, making relative paths impossible.
            // C# executable has no data on it's parent directory, so the only way to dynamically find the database is something like the following.
            // That doesn't mean I'm proud of it.
            string[] path = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);

            int i = 0;
            while (i < path.Length)
            {
                if (path[i] == "fleetmanager-csharp")
                {
                    break;
                }
                i++;
            }
            List<string> pathList = path.Take(i+1).ToList();
            pathList.Add("Eatech.FleetManager.ApplicationCore");
            pathList.Add("database.sqlite");

            string databasePath = String.Join('/', pathList.ToArray());
            DBConnection = new SqliteConnection(
                String.Format(
                    "Data Source={0};",
                    databasePath));
            DBConnection.Open();
        }


        public async Task<IEnumerable<Car>> GetAll(
            int? minYear = null,
            int? maxYear = null,
            string model = null,
            string manufacturer = null)
        {
            string query = "SELECT * FROM cars";
            string queryExtention = "";

            if (minYear != null)
            {
                queryExtention = String.Format(" WHERE ModelYear > {0}", minYear);
            }

            if (maxYear != null)
            {
                queryExtention += queryExtention == "" ? " WHERE " : " AND ";
                queryExtention += String.Format("ModelYear < {0}", maxYear);
            }
            if (model != null)
            {
                queryExtention += queryExtention == "" ? " WHERE " : " AND ";
                queryExtention += String.Format("Model = '{0}'", model);
            }
            if (manufacturer != null)
            {
                queryExtention += queryExtention == "" ? " WHERE " : " AND ";
                queryExtention += String.Format("Manufacturer = '{0}'", manufacturer);
            }

            query += queryExtention;

            SqliteCommand command = new SqliteCommand(query, DBConnection);
            SqliteDataReader reader = await command.ExecuteReaderAsync();
            return CarsFromQuery(reader);
        }

        public async Task<Car> Get(Guid id)
        {
            string query = String.Format(
                "SELECT * FROM cars WHERE Id = '{0}'", 
                id);
            SqliteCommand command = new SqliteCommand(query, DBConnection);
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
            Guid id, 
            int? modelYear = null, 
            string model = null, 
            string manufacturer = null, 
            string registration = null, 
            DateTime? inspectionDate = null, 
            int? engineSize = null, 
            int? enginePower = null)
        {
            Car car = await Remove(id) ?? new Car();

            car.Id = id;
            car.ModelYear = modelYear ?? car.ModelYear;
            car.Model = model ?? car.Model;
            car.Manufacturer = manufacturer ?? car.Manufacturer;
            car.Registration = registration ?? car.Registration;
            car.InspectionDate = inspectionDate ?? car.InspectionDate;
            car.EngineSize = engineSize ?? car.EngineSize;
            car.EnginePower = enginePower ?? car.EnginePower;

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
                    ModelYear = int.TryParse(reader["ModelYear"].ToString(), out int year) ? (int?) year : null,
                    Model = reader["Model"]?.ToString(),
                    Manufacturer = reader["Manufacturer"]?.ToString(),
                    Registration = reader["Registration"]?.ToString(),
                    InspectionDate = DateTime.TryParse(reader["InspectionDate"].ToString(), out DateTime inspectionDate) ? (DateTime?) inspectionDate : null,
                    EngineSize = int.TryParse(reader["EngineSize"].ToString(), out int engineSize) ? (int?) engineSize : null,
                    EnginePower = int.TryParse(reader["EnginePower"].ToString(), out int EnginePower) ? (int?) EnginePower : null
                });
            }
        return cars;
        }
    }
}