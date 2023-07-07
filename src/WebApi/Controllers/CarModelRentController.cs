using Application.CarBrands.Create;
using Application.CarModelRents.Create;
using Application.CarModelRents.Update;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.CarModelRents;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarModelRentController : ApiController
{
    private ILogger<CarModelRentController> _logger;

    public CarModelRentController(ILogger<CarModelRentController> logger, ISender sender)
        : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCarModelRentRequest request)
    {
        _logger.LogInformation("Started CarModelRentController.Create");

        var command = new CarModelRentCreateCommand(
            request.ValidFrom,
            request.ValidUntil,
            request.PricePerDay,
            request.Discount,
            request.IsVisible,
            request.CarModelId);

        Result<Guid> response = await Sender.Send(command);

        _logger.LogInformation("Finished CarModelRentController.Create");

        if (response.IsFailure)
        {
            return HandleFailure(response);
        }
        return CreatedAtAction(
            nameof(Create),
            new { id = response.Value },
            response.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCarModelRentRequest request)
    {
        _logger.LogInformation("Started CarModelRentController.Update");

        var command = new CarModelRentUpdateCommand(
            request.CarModelRentId,
            request.ValidFrom,
            request.ValidUntil,
            request.PricePerDay,
            request.Discount,
            request.IsVisible,
            request.CarModelId);

        Result<bool> response = await Sender.Send(command);

        _logger.LogInformation("Finished CarModelRentController.Update");

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
