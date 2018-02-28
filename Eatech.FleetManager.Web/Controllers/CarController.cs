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
            return (await _carService.GetAll()).Select(car => new CarDto (car));
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

            return Ok(new CarDto(car));
        }
        /// <summary>
        ///     Example HTTP POST: api/car/570890e2-8007-4e5c-a8d6-c3f670d8a9be
        ///     Example adds a car with id 570890e2-8007-4e5c-a8d6-c3f670d8a9be, null year, null model and null manufacturer.
        /// </summary>
        [HttpPost("{id}")]
        public async Task<IActionResult> Update(Guid id)
        {
            // Some authentication should take place in the final product.
            // Sanitation too
            var car = await _carService.Get(id);
            if (car == null)
            {
                // Car with this id doesn't exist, add.
                return Ok(new CarDto(await _carService.Add(
                    id,
                    ModelYear: (int?)Int32.Parse(Request.Form["ModelYear"].ToString()),
                    Model: Request.Form["Model"],
                    Manufacturer: Request.Form["Manufacturer"]
                    )));
            }
            else
            {
                // Car with this id exists, update with given info
                return Ok(new CarDto(await _carService.Update(
                    id,
                    ModelYear: (int?)Int32.Parse(Request.Form["ModelYear"].ToString()),
                    Model: Request.Form["Model"],
                    Manufacturer: Request.Form["Manufacturer"]
                    )));
            }
        }

        /// <summary>
        ///     Example HTTP DELETE: api/car/570890e2-8007-4e5c-a8d6-c3f670d8a9be
        ///     Removes the car with given id if it exists.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var car = await _carService.Remove(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(new CarDto(car));
        }
    }
}