using Application.CarBrands.Create;
using Application.CarBrands.GetAll;
using Application.CarBrands.GetById;
using Application.CarBrands.Update;
using Azure.Core;
using Domain.CarBrand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.CarBrands;

namespace WebApi.Controllers
{
    [Route("api/carbrand")]
    [ApiController]
    public class CarBrandController : ControllerBase
    {
        private ILogger<CarBrandController> _logger;
        private ISender _sender;

        public CarBrandController(ILogger<CarBrandController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateCarBrandRequest request)
        {
            _logger.LogInformation("Started CarBrandController.Create");

            var command = new CarBrandCreateCommand(request.carBrandName);

            await _sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.Create");

        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<CarBrand>> GetAll()
        {
            _logger.LogInformation("Started CarBrandController.GetAll");

            var command = new CarBrandGetAllCommand();

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.GetAll");


            return response;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<CarBrand> GetById(Guid id)
        {
            _logger.LogInformation("Started CarBrandController.GetById");

            var command = new CarBrandGetByIdCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.GetById");


            return response;
        }

        [HttpPut]
        public async Task Update(UpdateCarBrandRequest request)
        {
            _logger.LogInformation("Started CarBrandController.Update");

            var command = new CarBrandUpdateCommand(request.id, request.carBrandName);

            await _sender.Send(command);

            _logger.LogInformation("Finished CarBrandController.Update");
        }
    }
}
