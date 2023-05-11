using Application.Colors.Create;
using Application.Colors.GetAll;
using Application.Colors.GetById;
using Application.Colors.Update;
using Domain.Color;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts.Colors;

namespace WebApi.Controllers
{
    [Route("api/color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private ILogger<ColorController> _logger;
        private ISender _sender;

        public ColorController(ILogger<ColorController> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpPost]
        public async Task Create(CreateColorRequest request)
        {
            _logger.LogInformation("Started ColorController.Create");

            var command = new ColorCreateCommand(request.colorName);

            await _sender.Send(command);

            _logger.LogInformation("Finished ColorController.Create");
        }

        [Route("getall")]
        [HttpGet]
        public async Task<List<Color>> GetAll()
        {
            _logger.LogInformation("Started ColorController.GetAll");

            var command = new ColorGetAllCommand();

            var response = await _sender.Send(command);

            _logger.LogInformation("Finished ColorController.GetAll");

            return response;

        }

        [Route("{id}")]
        [HttpGet]
        public async Task<Color?> GetById(Guid id)
        {
            _logger.LogInformation("Started ColorController.GetById");

            var command = new ColorGetByIdCommand(id);

            var response = await _sender.Send(command); 

            _logger.LogInformation("Finished ColorController.GetById");

            return response;
        }

        [HttpPut]
        public async Task Update(UpdateColorRequest request)
        {
            _logger.LogInformation("Started ColorController.Update");

            var command = new ColorUpdateCommand(request.id, request.colorName);

            await _sender.Send(command);

            _logger.LogInformation("Finished ColorController.Update");

        }

    }
}
