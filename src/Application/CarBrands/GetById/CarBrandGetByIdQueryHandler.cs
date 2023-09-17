using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarBrands;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetById;

internal sealed class CarBrandGetByIdQueryHandler : IQueryHandler<CarBrandGetByIdQuery, CarBrandCarModelDto?>
{
    private ILogger<CarBrandGetByIdQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private IMapper _mapper;


    public CarBrandGetByIdQueryHandler(ILogger<CarBrandGetByIdQueryHandler> logger, ICarBrandRepository carBrandRepository, IMapper mapper)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _mapper = mapper;
    }

    public async Task<Result<CarBrandCarModelDto?>> Handle(CarBrandGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetByIdCommandHandler");

        try
        {
            var dbCarBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            if (dbCarBrand == null ||dbCarBrand is null)
            {

                _logger.LogWarning("CarBrandGetByIdCommandHandler: CarBrand doesn't exist!");
                return Result.Failure<CarBrandCarModelDto?>(new Error(
                    "CarBrand.NotFound",
                    $"The CarBrand with Id {request.CarBrandId} was not found"));
            }

            var resultDto = _mapper.Map<CarBrand, CarBrandCarModelDto>(dbCarBrand);

            _logger.LogInformation("Finished CarBrandGetByIdCommandHandler");
            return resultDto;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarBrandCarModelDto?>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
