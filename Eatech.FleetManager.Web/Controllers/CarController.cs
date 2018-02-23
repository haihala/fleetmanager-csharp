using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eatech.FleetManager.ApplicationCore.Entities;
using Eatech.FleetManager.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eatech.FleetManager.Web.Controllers
{
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        /// <summary>
        ///     Example HTTP GET: api/car
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<CarDto>> Get()
        {
            return (await _carService.GetAll()).Select(c => new CarDto
            {
                Id = c.Id,
                ModelYear = c.ModelYear
            });
        }

        /// <summary>
        ///     Example HTTP GET: api/car/570890e2-8007-4e5c-a8d6-c3f670d8a9be
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var car = await _carService.Get(id);
            if (car == null)
            {
                return NotFound();
            }

            return Ok(new CarDto
            {
                Id = car.Id,
                ModelYear = car.ModelYear
            });
        }
    }
}