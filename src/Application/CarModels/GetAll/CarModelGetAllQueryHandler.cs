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

namespace Application.CarModels.GetAll;

internal sealed class CarModelGetAllQueryHandler : IQueryHandler<CarModelGetAllQuery, List<CarModelDto>>
{
    private ILogger<CarModelGetAllQueryHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private IMapper _mapper;

    public CarModelGetAllQueryHandler(
        ILogger<CarModelGetAllQueryHandler> logger,
        ICarModelRepository carModelRepository,
        IMapper mapper)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
        _mapper = mapper;
    }
    public async Task<Result<List<CarModelDto>>> Handle(CarModelGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetAllCommandHandler");

        try
        {
            var carModels = await _carModelRepository.GetAllAsync(cancellationToken);

            if (!carModels.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarModels in database");
                return Result.Failure<List<CarModelDto>>(new Error(
                        "CarModel.NoData",
                        "There are no CarModels to fetch"));
            }

            _logger.LogInformation("Finished CarModelGetAllCommandHandler");

            var resultDto = _mapper.Map<List<CarModel>,List<CarModelDto>>(carModels);

            return resultDto;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarModelDto>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
