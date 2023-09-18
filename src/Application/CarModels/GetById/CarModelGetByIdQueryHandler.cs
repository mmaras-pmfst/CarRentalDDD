using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarModels;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetById;

internal sealed class CarModelGetByIdQueryHandler : IQueryHandler<CarModelGetByIdQuery, CarModelDetailDto?>
{
    private ILogger<CarModelGetByIdQueryHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private IMapper _mapper;


    public CarModelGetByIdQueryHandler(
        ILogger<CarModelGetByIdQueryHandler> logger,
        ICarModelRepository carModelRepository,
        IMapper mapper)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }

    public async Task<Result<CarModelDetailDto?>> Handle(CarModelGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetByIdCommandHandler");

        try
        {
            var carModel = await _carModelRepository.GetByIdAsync(request.CarModelId, cancellationToken);

            if(carModel is null ||carModel == null)
            {
                _logger.LogWarning("CarModelGetByIdCommandHandler: CarModel doesn't exist!");
                return Result.Failure<CarModelDetailDto?>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var resultDto = _mapper.Map<CarModel, CarModelDetailDto>(carModel);

            _logger.LogInformation("Finished CarModelGetByIdCommandHandler");

            return resultDto;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarModelDetailDto?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
