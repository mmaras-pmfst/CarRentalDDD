using Application.Offices.Create;
using Application.Offices.GetAll;
using Application.Offices.GetById;
using Application.Offices.Update;
using Domain.Office;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Offices;

namespace WebApi.Controllers
{
    [Route("api/office")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private ILogger<OfficeController> _logger;
        private ISender _sender;

        public OfficeController(ISender sender, ILogger<OfficeController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpPost]
        public async Task Create(CreateOfficeRequest request)
        {
            _logger.LogInformation("Started OfficeController.Create");
            var command = new OfficeCreateCommand(request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

            await _sender.Send(command);

            _logger.LogInformation("Finished OfficeController.Create");

        }

        [HttpGet]
        public async Task<List<Office>> GetAll()
        {
            _logger.LogInformation("Started OfficeController.GetAll");

            var command = new OfficeGetAllCommand();

            var result = await _sender.Send(command);

            _logger.LogInformation("Finished OfficeController.GetAll");

            return result;

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Office?> GetById(Guid id)
        {
            _logger.LogInformation("Started OfficeController.GetById");

            var command = new OfficeGetByIdCommand(id);

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished OfficeController.GetById");

            return response;
        }

        [HttpPut]
        public async Task Update(UpdateOfficeRequest request)
        {
            _logger.LogInformation("Started OfficeController.Update");

            var command = new OfficeUpdateCommand(request.id, request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

            await _sender.Send(command);

            _logger.LogInformation("Finished OfficeController.Update");

        }
    }
}
