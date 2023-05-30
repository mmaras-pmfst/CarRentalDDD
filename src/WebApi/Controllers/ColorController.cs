using Application.Colors.Create;
using Application.Colors.GetAll;
using Application.Colors.GetById;
using Application.Colors.Update;
using Domain.Color;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Colors;

namespace WebApi.Controllers
{
    [Route("api/color")]
    [ApiController]
    public class ColorController : ApiController
    {
        private ILogger<ColorController> _logger;

        public ColorController(ILogger<ColorController> logger, ISender sender)
            :base(sender)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateColorRequest request)
        {
            _logger.LogInformation("Started ColorController.Create");

            var command = new ColorCreateCommand(request.colorName);

            Result<Guid> response = await Sender.Send(command);

            _logger.LogInformation("Finished ColorController.Create");

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
            _logger.LogInformation("Started ColorController.GetAll");

            var command = new ColorGetAllQuery();

            Result<List<Color>> response = await Sender.Send(command);

            _logger.LogInformation("Finished ColorController.GetAll");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            _logger.LogInformation("Started ColorController.GetById");

            var command = new ColorGetByIdQuery(id);

            Result<Color?> response = await Sender.Send(command); 

            _logger.LogInformation("Finished ColorController.GetById");

            return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateColorRequest request)
        {
            _logger.LogInformation("Started ColorController.Update");

            var command = new ColorUpdateCommand(request.id, request.colorName);

            Result<bool> response = await Sender.Send(command);

            _logger.LogInformation("Finished ColorController.Update");

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
