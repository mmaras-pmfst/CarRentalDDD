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

namespace Application.Cars.GetAll;
internal class CarGetAllQueryHandler : IQueryHandler<CarGetAllQuery, List<CarDto>>
{
    private ILogger<CarGetAllQueryHandler> _logger;
    private readonly ICarRepository _carRepository;
    private IMapper _mapper;

    public CarGetAllQueryHandler(ILogger<CarGetAllQueryHandler> logger, ICarRepository carRepository, IMapper mapper)
    {
        _logger = logger;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CarDto>>> Handle(CarGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarGetAllQueryHandler");

        try
        {

            var dbCars = await _carRepository.GetAllAsync(cancellationToken);

            if (!dbCars.Any())
            {
                _logger.LogWarning("CarGetAllQueryHandler: No Cars in database");
                return Result.Failure<List<CarDto>>(new Error(
                        "Car.NoData",
                        "There are no Cars to fetch"));
            }
            var resultDto = _mapper.Map<List<Car>,List<CarDto>>(dbCars);
            _logger.LogInformation("Finished CarGetAllQueryHandler");

            return resultDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<CarDto>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
