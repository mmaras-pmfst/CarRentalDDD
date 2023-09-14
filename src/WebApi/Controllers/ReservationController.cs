using Application.Reservations.AddReservationItem;
using Application.Reservations.Create;
using Application.Reservations.GetAll;
using Application.Reservations.GetById;
using Application.Reservations.RemoveReservationItem;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.Reservations;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReservationController : ApiController
{
    private ILogger<ReservationController> _logger;
    public ReservationController(ILogger<ReservationController> logger, ISender sender)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateReservationRequest request)
    {
        _logger.LogInformation("Started ReservationController.Create");
        var extrases = new List<ExtrasModel>();
        if (request.Extras != null || request.Extras.Any())
        {
            request.Extras.ForEach(x => extrases.Add(new ExtrasModel(x.ExtraId, x.Quantity)));

        }
        var commmand = new ReservationCreateCommand(
            request.DriverFirstName,
            request.DriverLastName,
            request.Email,
            request.PickUpDate,
            request.DropDownDate,
            request.PickUpLocationId,
            request.DropDownLocationId,
            request.CarModelId,
            extrases);

        var response = await Sender.Send(commmand);

        _logger.LogInformation("Finished ReservationController.Create");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }

    [HttpPost("addReservationItem")]
    public async Task<IActionResult> AddReservationItem(CreateReservationItemRequest request)
    {
        _logger.LogInformation("Started ReservationController.AddReservationItem");
        var extrases = new List<ExtrasModel>();
        if (request.Extras != null || request.Extras.Any())
        {
            request.Extras.ForEach(x => extrases.Add(new ExtrasModel(x.ExtraId, x.Quantity)));

        }
        var command = new AddReservationItemCommand(request.ReservationId, extrases);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ReservationController.AddReservationItem");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }

    [HttpDelete("removeReservationItem")]
    public async Task<IActionResult> RemoveReservationItem(RemoveReservationItemRequest request)
    {
        _logger.LogInformation("Started ReservationController.RemoveReservationItem");
        var command = new RemoveReservationItemCommand(request.ReservationId, request.ExtraIds);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ReservationController.RemoveReservationItem");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);

    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] GetAllReservationsRequest request)
    {
        _logger.LogInformation("Started ReservationController.GetAll");

        var command = new ReservationGetAllQuery(request.DateFrom, request.DateTo);
        var response = await Sender.Send(command);

        _logger.LogInformation("Finished ReservationController.GetAll");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Started ReservationController.GetById");

        var command = new ReservationGetByIdQuery(id);
        var response = await Sender.Send(command);
        _logger.LogInformation("Finished ReservationController.GetById");
        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);



    }
}
