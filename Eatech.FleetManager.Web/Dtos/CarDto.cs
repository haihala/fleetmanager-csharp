using System;

namespace Eatech.FleetManager.ApplicationCore.Entities
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public int? ModelYear { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string Registration { get; set; }

        public DateTime? InspectionDate { get; set; }

        public int? EngineSize { get; set; }

        public int? EnginePower { get; set; }


        public CarDto (Car car)
        {
            // Makes CarController a bit neater.
            Id = car.Id;
            
            ModelYear = car.ModelYear;
            Model = car.Model;
            Manufacturer = car.Manufacturer;

            Registration = car.Registration;
            InspectionDate = car.InspectionDate;
            EngineSize = car.EngineSize;
            EnginePower = car.EnginePower;
        }
    }
}