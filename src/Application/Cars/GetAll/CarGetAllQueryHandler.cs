using Application.Abstractions;
using Domain.Management.Car;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.GetAll;
internal class CarGetAllQueryHandler : IQueryHandler<CarGetAllQuery, List<Car>>
{
    private ILogger<CarGetAllQueryHandler> _logger;
    private readonly ICarRepository _carRepository;

    public CarGetAllQueryHandler(ILogger<CarGetAllQueryHandler> logger, ICarRepository carRepository)
    {
        _logger = logger;
        _carRepository = carRepository;
    }

    public async Task<Result<List<Car>>> Handle(CarGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarGetAllQueryHandler");

        try
        {

            var dbCars = await _carRepository.GetAllAsync(cancellationToken);

            if (!dbCars.Any())
            {
                _logger.LogWarning("CarGetAllQueryHandler: No Cars in database");
                return Result.Failure<List<Car>>(new Error(
                        "Car.NoData",
                        "There are no Cars to fetch"));
            }

            _logger.LogInformation("Finished CarGetAllQueryHandler");

            return dbCars;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Car>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
