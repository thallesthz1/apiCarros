using api_car.Domains.Interface;
using api_car.Domains.Model;
using api_car.Web.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_car.Web.Controller
{
    [ApiController]
    [Route("v1/car")]
    public class CarController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICarRepository repository;

        public CarController(
            IMapper mapper,
            ICarRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet]
        [Route("count")]
        public async Task<long> Count()
        {
            return await repository.CountAsync();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarOutputModelDTO))]
        public async Task<ActionResult> Get()
        {
            var car = await repository.GetAllAsync();

            return Ok(car);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarOutputModelDTO))]
        public async Task<ActionResult> GetById(string carId)
        {
            var car = await repository.GetByIdAsync(carId);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<CarOutputModelDTO>(car));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarInputModelDTO))]
        public async Task<ActionResult> Post([FromForm] CarInputModelDTO carInputModelDTO)
        {
            Car car = mapper.Map<Car>(carInputModelDTO);

            await repository.CreateAsync(car);

            return CreatedAtAction(nameof(GetById), new { CarId = car.CarId.ToString() }, car);
        }

        [HttpPut]
        [Route("{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CarInputModelDTO))]
        public async Task<ActionResult> Put(string carId, [FromForm] CarInputModelDTO carInputModelDTO)
        {
            var existingCar = await repository.GetByIdAsync(carId);

            if (existingCar == null)
            {
                return NotFound();
            }

            var car = mapper.Map<Car>(carInputModelDTO);

            car.CarId = carId;

            await repository.UpdateAsync(car);

            return NoContent();
        }

        [HttpDelete]
        [Route("{carId}")]
        public async Task<ActionResult> Delete(string carId)
        {
            var existingCar = await repository.GetByIdAsync(carId);

            if (existingCar == null)
            {
                return NoContent();
            }

            await repository.DeleteAsync(carId);

            return NoContent();
        }
    }
}
