using Application.CarModels.Create;
using Application.CarModels.GetAll;
using Application.CarModels.GetById;
using Application.CarModels.Update;
using Application.Cars.Create;
using Application.Cars.GetAll;
using Application.Cars.GetById;
using Application.Cars.Update;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Contracts.CarModels;
using WebApi.Contracts.Cars;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarController : ApiController
{
    private ILogger<CarController> _logger;
    public CarController(ISender sender, ILogger<CarController> logger) : base(sender)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCarRequest request)
    {
        _logger.LogInformation("Started CarController.Create");

        var command = new CarCreateCommand(
            request.NumberPlate,
            request.Name,
            request.Kilometers,
            request.Image,
            request.CarStatus,
            request.FuelType,
            request.CarModelId,
            request.OfficeId);

        Result<Guid> response = await Sender.Send(command);

        _logger.LogInformation("Finished CarController.Create");

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
        _logger.LogInformation("Started CarController.GetAll");

        var command = new CarGetAllQuery();

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished CarController.GetAll");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);


    }

    [Route("{carId}")]
    [HttpGet]
    public async Task<IActionResult> GetById(Guid carId)
    {
        _logger.LogInformation("Started CarController.GetById");

        var command = new CarGetByIdQuery(carId);

        var response = await Sender.Send(command);

        _logger.LogInformation("Finished CarController.GetById");

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);

    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCarRequest request)
    {
        _logger.LogInformation("Started CarController.Update");

        var command = new CarUpdateCommand(
            request.CarId,
            request.Kilometers,
            request.Image,
            request.CarStatus,
            request.OfficeId);

        Result<bool> response = await Sender.Send(command);

        _logger.LogInformation("Finished CarController.Update");

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
