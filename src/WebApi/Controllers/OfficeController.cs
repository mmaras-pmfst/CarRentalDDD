using Application.Office.Create;
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
            var command = new CreateOfficeCommand(request.country, request.city, request.streetName, request.streetNumber, request.openingTime, request.closingTime, request.phoneNumber);

            await _sender.Send(command);

            _logger.LogInformation("Finished OfficeController.Create");

        }
    }
}
