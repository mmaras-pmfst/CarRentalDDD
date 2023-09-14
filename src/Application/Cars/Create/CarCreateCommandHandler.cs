using Application.Abstractions;
using Domain.Management.Cars;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Create;
internal class CarCreateCommandHandler : ICommandHandler<CarCreateCommand, Guid>
{
    private ILogger<CarCreateCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCreateCommandHandler(ILogger<CarCreateCommandHandler> logger, ICarRepository carRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carRepository = carRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CarCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCreateCommandHandler");

        try
        {
            var exists = await _carRepository.PlateNumberAlreadyExists(request.NumberPlate, cancellationToken);

            if (exists)
            {
                //car already exists!
            }

            var newCar = Car.Create(
                Guid.NewGuid(),
                request.NumberPlate,
                request.Name,
                request.Kilometers,
                request.Image,
                request.CarStatus,
                request.FuelType,
                request.CarModelId,
                request.OfficeId);

            await _carRepository.AddAsync(newCar, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarCreateCommandHandler");
            return newCar.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
