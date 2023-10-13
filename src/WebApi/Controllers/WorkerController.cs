using Application.Workers.Create;
using Application.Workers.GetAll;
using Application.Workers.GetById;
using Application.Workers.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Workers;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WorkerController : ApiController
{
    private ILogger<WorkerController> _logger;

    public WorkerController(ILogger<WorkerController> logger, ISender sender)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkerRequest request)
    {
        _logger.LogInformation("Started WorkerController.Create");

        var command = new WorkerCreateCommand(
            request.PersonalIdentificationNumber,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.OfficeId);

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished WorkerController.Create");

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
        _logger.LogInformation("Started WorkerController.GetAll");

        var command = new WorkerGetAllQuery();

        var response = await Sender.Send(command);  

        _logger.LogInformation("Finished WorkerController.GetAll");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Started WorkerController.GetById");

        var command = new WorkerGetByIdQuery(id);

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished WorkerController.GetById");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateWorkerRequest request)
    {
        _logger.LogInformation("Started WorkerController.GetById");

        var command = new WorkerUpdateCommand(request.WorkerId, request.Email, request.PhoneNumber, request.OfficeId);

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished WorkerController.GetById");

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

