using Application.Offices.Create;
using Application.Offices.GetAll;
using Application.Offices.GetById;
using Application.Offices.Update;
using Domain.Management.Office;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Offices;

namespace WebApi.Controllers
{
    [Route("api/office")]
    [ApiController]
    public class OfficeController : ApiController
    {
        private ILogger<OfficeController> _logger;

        public OfficeController(ISender sender, ILogger<OfficeController> logger)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOfficeRequest request)
        {
            _logger.LogInformation("Started OfficeController.Create");
            var command = new OfficeCreateCommand(request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

            Result<Guid> response = await Sender.Send(command);

            _logger.LogInformation("Finished OfficeController.Create");

            if (response.IsFailure)
            {
                return HandleFailure(response);
            }
            return CreatedAtAction(
                nameof(Create),
                new { id = response.Value },
                response.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Started OfficeController.GetAll");

            var command = new OfficeGetAllQuery();

            Result<List<Office>> response = await Sender.Send(command);

            _logger.LogInformation("Finished OfficeController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started OfficeController.GetById");

            var command = new OfficeGetByIdQuery(id);

            Result<Office?> response = await Sender.Send(command);

            _logger.LogInformation("Finished OfficeController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateOfficeRequest request)
        {
            _logger.LogInformation("Started OfficeController.Update");

            var command = new OfficeUpdateCommand(request.id, request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

            Result<bool> response = await Sender.Send(command);

            _logger.LogInformation("Finished OfficeController.Update");

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
