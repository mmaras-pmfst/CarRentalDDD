using Application.CarModels.Create;
using Application.CarModels.GetAll;
using Application.CarModels.GetById;
using Application.CarModels.Update;
using Domain.Management.CarModels;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.CarModels;

namespace WebApi.Controllers
{
    [Route("api/carmodel")]
    [ApiController]
    public class CarModelController : ApiController
    {
        private ILogger<CarModelController> _logger;

        public CarModelController(ILogger<CarModelController> logger, ISender sender)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarModelRequest request)
        {
            _logger.LogInformation("Started CarModelController.Create");

            var command = new CarModelCreateCommand(
                request.CarModelName,
                request.PricePerDay,
                request.Discount,
                request.CarBrandId,
                request.CarCategoryId);

            Result<Guid> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarModelController.Create");

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Create),
                new { id = response.Value },
                response.Value);

        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Started CarModelController.GetAll");

            var command = new CarModelGetAllQuery();

            Result<List<CarModel>> response = await Sender.Send(command); 

            _logger.LogInformation("Finished CarModelController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


        }

        [Route("{carModelId}")]
        [HttpGet]
        public async Task<IActionResult> GetById( Guid carModelId)
        {
            _logger.LogInformation("Started CarModelController.GetById");

            var command = new CarModelGetByIdQuery(carModelId);

            Result<CarModel?> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarModelController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarModelRequest request)
        {
            _logger.LogInformation("Started CarModelController.Update");

            var command = new CarModelUpdateCommand(
                request.CarModelId,
                request.PricePerDay,
                request.Discount,
                request.CarModelName,
                request.CarCategoryId);

            Result<bool> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarModelController.Update");

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Update),
                new { id = response.Value },
                response.Value);
        }
    }
}
