using System;

namespace Eatech.FleetManager.ApplicationCore.Entities
{
    public class Car
    {
        public Guid Id { get; set; }

        public int? ModelYear { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }

        public string Registration { get; set; }

        public DateTime? InspectionDate { get; set; }

        public int? EngineSize { get; set; }

        public int? EnginePower { get; set; }

        public bool Equals(Car other)
        {
            // Code itself doesn't need this at any point, used for tests.
            return (
                Id == other.Id &&
                ModelYear == other.ModelYear &&
                Model == other.Model &&
                Manufacturer == other.Manufacturer &&
                Registration == other.Registration &&
                InspectionDate == other.InspectionDate &&
                EngineSize == other.EngineSize &&
                EnginePower == other.EnginePower);
        }
    }
}