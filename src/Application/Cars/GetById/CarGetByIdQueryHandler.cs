using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
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
internal class CarGetByIdQueryHandler : IQueryHandler<CarGetByIdQuery, CarDetailDto?>
{
    private ILogger<CarGetByIdQueryHandler> _logger;
    private readonly ICarRepository _carRepository;
    private IMapper _mapper;

    public CarGetByIdQueryHandler(ILogger<CarGetByIdQueryHandler> logger, ICarRepository carRepository, IMapper mapper)
    {
        _logger = logger;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<Result<CarDetailDto?>> Handle(CarGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarGetByIdQueryHandler");

        try
        {
            var dbCar = await _carRepository.GetByIdAsync(request.CarId, cancellationToken);
            if (dbCar == null || dbCar is null )
            {
                _logger.LogWarning("CarGetByIdQueryHandler: Car doesn't exist!");
                return Result.Failure<CarDetailDto?>(new Error(
                    "Car.NotFound",
                    $"The Car with Id {request.CarId} was not found"));
            }

            var resultDto = _mapper.Map<Car, CarDetailDto>(dbCar);

            _logger.LogInformation("Finished CarGetByIdQueryHandler");
            return resultDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarGetByIdQueryHandler error: {0}", ex.Message);
            return Result.Failure<CarDetailDto?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
