using System;

namespace Eatech.FleetManager.ApplicationCore.Entities
{
    public class CarDto
    {
        public Guid Id { get; set; }

        public int ModelYear { get; set; }

        public string Model { get; set; }

        public string Manufacturer { get; set; }
    }
}