using Application.CarBrands.Create;
using Application.CarBrands.GetAll;
using Application.CarBrands.GetById;
using Application.CarBrands.Update;
using Azure.Core;
using Domain.Management.CarBrand;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.CarBrands;

namespace WebApi.Controllers
{
    [Route("api/carbrand")]
    [ApiController]
    public class CarBrandController : ApiController
    {
        private ILogger<CarBrandController> _logger;

        public CarBrandController(ILogger<CarBrandController> logger, ISender sender)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarBrandRequest request)
        {
            _logger.LogInformation("Started CarBrandController.Create");

            var command = new CarBrandCreateCommand(request.carBrandName);

            Result<Guid> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.Create");

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Create),
                new { id = response.Value },
                response.Value);

        }

        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Started CarBrandController.GetAll");

            var command = new CarBrandGetAllQuery();

            Result<List<CarBrand>> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started CarBrandController.GetById");

            var command = new CarBrandGetByIdQuery(id);

            Result<CarBrand?> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarBrandRequest request)
        {
            _logger.LogInformation("Started CarBrandController.Update");

            var command = new CarBrandUpdateCommand(request.id, request.carBrandName);

            Result<bool> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.Update");

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
