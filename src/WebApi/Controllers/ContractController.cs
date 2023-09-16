using Application.Contracts.AddContractItem;
using Application.Contracts.Create;
using Application.Contracts.GetAll;
using Application.Contracts.GetById;
using Application.Contracts.RemoveContractItem;
using Application.Reservations.Create;
using Domain.Shared.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Contracts;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ContractController : ApiController
{
    private ILogger<ContractController> _logger;

    public ContractController(ISender sender, ILogger<ContractController> logger)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContractRequest request)
    {
        _logger.LogInformation("Started ContractController.Create");
        var extrases = new List<ExtrasModel>();
        if (request.Extras != null || request.Extras.Any())
        {
            request.Extras.ForEach(x => extrases.Add(new ExtrasModel(x.ExtraId, x.Quantity)));
        }
        var commmand = new CreateContractCommand(
            request.DriverFirstName,request.DriverLastName,request.Email,
            request.PickUpDate, request.DropDownDate,request.PickUpOfficeId,
            request.DropDownOfficeId,request.CarId,request.DriverLicenceNumber,
            request.DriverIdentificationNumber,request.CardType, request.PaymentMethod,
            request.ReservationId,request.WorkerId,request.CardName,request.CardNumber,
            request.CVV,request.CardDateExpiration,request.CardYearExpiration,extrases);

        var response = await Sender.Send(commmand);
        _logger.LogInformation("Finished ContractController.Create");
        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }

    [HttpPost("addContractItem")]
    public async Task<IActionResult> AddContractItem(CreateContractItemRequest request)
    {
        _logger.LogInformation("Started ContractController.AddContractItem");
        var extrases = new List<ExtrasModel>();
        if (request.Extras != null || request.Extras.Any())
        {
            request.Extras.ForEach(x => extrases.Add(new ExtrasModel(x.ExtraId, x.Quantity)));

        }
        var command = new AddContractItemCommand(request.ContractId, extrases);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ContractController.AddContractItem");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }

    [HttpDelete("removeContractItem")]
    public async Task<IActionResult> RemoveContractItem(RemoveContractItemRequest request)
    {
        _logger.LogInformation("Started ContractController.RemoveContractItem");
        var command = new RemoveContractItemCommand(request.ContractId, request.ExtraIds);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ContractController.RemoveContractItem");

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
    public async Task<IActionResult> GetAll([FromQuery] GetAllContractRequest request)
    {
        _logger.LogInformation("Started ContractController.GetAll");

        var command = new GetAllContractQuery(request.DateFrom, request.DateTo);
        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ContractController.GetAll");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Started ContractController.GetById");
        var command = new GetByIdContractQuery(id);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ContractController.GetById");
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}
