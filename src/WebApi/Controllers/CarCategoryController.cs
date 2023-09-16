using Application.CarCategories.Create;
using Application.CarCategories.GetAll;
using Application.CarCategories.GetById;
using Application.CarCategories.Update;
using Azure.Core;
using Domain.Management.CarCategories;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.CarCategories;

namespace WebApi.Controllers
{
    [Route("api/carcategory")]
    [ApiController]
    public class CarCategoryController : ApiController
    {
        private ILogger<CarCategoryController> _logger;

        public CarCategoryController(ILogger<CarCategoryController> logger, ISender sender)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCarCategoryRequest request)
        {
            _logger.LogInformation("Started CarCategoryController.Create");

            var command = new CarCategoryCreateCommand(request.name, request.shortName, request.description);

            Result<Guid> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.Create");

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
            _logger.LogInformation("Started CarCategoryController.GetAll");

            var command = new CarCategoryGetAllQuery();

            Result<List<CarCategory>> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started CarCategoryController.GetById");

            var command = new CarCategoryGetByIdQuery(id);

            Result<CarCategory?> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCarCategoryRequest request)
        {
            _logger.LogInformation("Started CarCategoryController.Update");

            var command = new CarCategoryUpdateCommand(request.id, request.name, request.shortName, request.description);

            Result<bool> response = await Sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.Update");

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
