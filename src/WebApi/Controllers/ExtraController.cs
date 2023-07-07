using Application.Extras.Create;
using Application.Extras.GetAll;
using Application.Extras.GetById;
using Application.Extras.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Extras;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ExtraController : ApiController
{
    private ILogger<ExtraController> _logger;

    public ExtraController(ILogger<ExtraController> logger, ISender sender)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateExtraRequest request)
    {
        _logger.LogInformation("Started ExtraController.Create");

        var command = new ExtrasCreateCommand(request.Name, request.Description, request.PricePerDay);
        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ExtraController.Create");

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
        _logger.LogInformation("Started ExtraController.GetAll");

        var command = new ExtrasGetAllQuery();

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ExtraController.GetAll");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Started ExtraController.GetAll");

        var command = new ExtrasGetByIdQuery(id);
        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ExtraController.GetAll");
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateExtraRequest request)
    {
        _logger.LogInformation("Started ExtraController.Update");

        var command = new ExtrasUpdateCommand(request.ExtraId, request.Name, request.Description, request.PricePerDay);

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ExtraController.Update");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }
}
