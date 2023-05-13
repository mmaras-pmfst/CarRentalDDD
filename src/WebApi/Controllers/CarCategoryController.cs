using Application.CarCategories.Create;
using Application.CarCategories.GetAll;
using Application.CarCategories.GetById;
using Application.CarCategories.Update;
using Azure.Core;
using Domain.CarCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.CarCategories;

namespace WebApi.Controllers
{
    [Route("api/carcategory")]
    [ApiController]
    public class CarCategoryController : ControllerBase
    {
        private ILogger<CarCategoryController> _logger;
        private ISender _sender;

        public CarCategoryController(ILogger<CarCategoryController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateCarCategoryRequest request)
        {
            _logger.LogInformation("Started CarCategoryController.Create");

            var command = new CarCategoryCreateCommand(request.name, request.shortName, request.description);

            await _sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.Create");
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<CarCategory>> GetAll()
        {
            _logger.LogInformation("Started CarCategoryController.GetAll");

            var command = new CarCategoryGetAllCommand();

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.GetAll");

            return response;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<CarCategory?> GetById(Guid id)
        {
            _logger.LogInformation("Started CarCategoryController.GetById");

            var command = new CarCategoryGetByIdCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.GetById");

            return response;
        }

        [HttpPut]
        public async Task Update(UpdateCarCategoryRequest request)
        {
            _logger.LogInformation("Started CarCategoryController.Update");

            var command = new CarCategoryUpdateCommand(request.id, request.name, request.shortName, request.description);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarCategoryController.Update");
        }
    }
}
