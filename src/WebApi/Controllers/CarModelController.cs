using Application.CarModels.Create;
using Application.CarModels.GetAll;
using Application.CarModels.GetById;
using Application.CarModels.Update;
using Domain.CarBrand.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.CarModels;

namespace WebApi.Controllers
{
    [Route("api/carmodel")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private ILogger<CarModelController> _logger;
        private ISender _sender;

        public CarModelController(ILogger<CarModelController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateCarModelRequest request)
        {
            _logger.LogInformation("Started CarModelController.Create");

            var command = new CarModelCreateCommand(request.CarModelName, request.BasePricePerDay, request.CarBrandId,  request.CarCategoryId);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarModelController.Create");

        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<List<CarModel>> GetAll()
        {
            _logger.LogInformation("Started CarModelController.GetAll");

            var command = new CarModelGetAllCommand();

            var response = await _sender.Send(command); 

            _logger.LogInformation("Finished CarModelController.GetAll");

            return response;

        }

        [Route("{carBrandId}/{carModelId}")]
        [HttpGet]
        public async Task<CarModel> GetById(Guid carBrandId, Guid carModelId)
        {
            _logger.LogInformation("Started CarModelController.GetById");

            var command = new CarModelGetByIdCommand(carBrandId, carModelId);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarModelController.GetById");

            return response;
        }

        [HttpPut]
        public async Task Update(UpdateCarModelRequest request)
        {
            _logger.LogInformation("Started CarModelController.Update");

            var command = new CarModelUpdateCommand(request.CarModelId, request.BasePricePerDay, request.CarModelName,  request.CarBrandId, request.CarCategoryId);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarModelController.Update");
        }
    }
}
