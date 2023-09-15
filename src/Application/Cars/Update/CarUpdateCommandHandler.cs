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

namespace Application.Cars.Update;
internal class CarUpdateCommandHandler : ICommandHandler<CarUpdateCommand, bool>
{
    private ILogger<CarUpdateCommandHandler> _logger;
    private readonly ICarRepository _carRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOfficeRepository _officeRepository;

    public CarUpdateCommandHandler(ILogger<CarUpdateCommandHandler> logger, ICarRepository carRepository, IUnitOfWork unitOfWork, IOfficeRepository officeRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
        _unitOfWork = unitOfWork;
        _officeRepository = officeRepository;
    }

    public async Task<Result<bool>> Handle(CarUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarUpdateCommandHandler");

        try
        {
            var car = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);

            if (car == null)
            {
                _logger.LogWarning("CarUpdateCommandHandler: Car doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "Car.NotFound",
                    $"The Car with Id {request.CarId} was not found"));
            }

            var office = await _officeRepository.GetByIdAsync(request.OfficeId, cancellationToken);

            if (office == null)
            {
                _logger.LogWarning("CarUpdateCommandHandler: Office doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "Office.NotFound",
                    $"The Office with Id {request.OfficeId} was not found"));
            }

            var result = car.Update(
                request.Kilometers,
                request.Image,
                request.CarStatus,
                office);
            if (result.IsFailure)
            {
                _logger.LogWarning("CarUpdateCommandHandler: Wrong car kilometers");
                return Result.Failure<bool>(new Error(
                    result.Error.Code,
                    result.Error.Message));
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CarUpdateCommandHandler");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
