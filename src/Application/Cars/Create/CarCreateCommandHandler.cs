using Application.Abstractions;
using Domain.Errors;
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
    private readonly ICarModelRepository _carModelRepository;
    private readonly IOfficeRepository _officeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCreateCommandHandler(
        ILogger<CarCreateCommandHandler> logger,
        ICarRepository carRepository,
        IUnitOfWork unitOfWork,
        ICarModelRepository carModelRepository,
        IOfficeRepository officeRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _unitOfWork = unitOfWork;
        _carModelRepository = carModelRepository;
        _officeRepository = officeRepository;
    }

    public async Task<Result<Guid>> Handle(CarCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCreateCommandHandler");

        try
        {
            var exists = await _carRepository.PlateNumberAlreadyExists(request.NumberPlate, cancellationToken);

            if (exists)
            {
                return Result.Failure<Guid>(DomainErrors.Car.CarAlreadyExists);

            }

            var carModel = await _carModelRepository.GetByIdAsync(request.CarModelId, cancellationToken);
            if(carModel == null)
            {
                _logger.LogWarning("CarCreateCommandHandler: CarModel doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var office = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);
            if (office == null)
            {
                _logger.LogWarning("CarCreateCommandHandler: Office doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.OfficeId} was not found"));
            }


            var newCar = Car.Create(
                Guid.NewGuid(),
                request.NumberPlate,
                request.Name,
                request.Kilometers,
                request.Image,
                request.CarStatus,
                request.FuelType,
                carModel,
                office);

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
