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

namespace Application.Cars.GetById;
internal class CarGetByIdQueryHandler : IQueryHandler<CarGetByIdQuery, Car?>
{
    private ILogger<CarGetByIdQueryHandler> _logger;
    private readonly ICarRepository _carRepository;

    public CarGetByIdQueryHandler(ILogger<CarGetByIdQueryHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task<Result<Car?>> Handle(CarGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarGetByIdQueryHandler");

        try
        {
            var dbCar = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);
            if (dbCar == null)
            {
                _logger.LogWarning("CarGetByIdQueryHandler: Car doesn't exist!");
                return Result.Failure<Car?>(new Error(
                    "Car.NotFound",
                    $"The Car with Id {request.CarId} was not found"));
            }

            _logger.LogInformation("Finished CarGetByIdQueryHandler");
            return dbCar;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<Car?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
