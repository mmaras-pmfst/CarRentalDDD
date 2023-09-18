using Application.Abstractions;
using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarCategories;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetById;

internal sealed class CarCategoryGetByIdQueryHandler : IQueryHandler<CarCategoryGetByIdQuery, CarCategoryDetailDto?>
{
    private ILogger<CarCategoryGetByIdQueryHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private IMapper _mapper;

    public CarCategoryGetByIdQueryHandler(ILogger<CarCategoryGetByIdQueryHandler> logger, ICarCategoryRepository carCategoryRepository, IMapper mapper)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<CarCategoryDetailDto?>> Handle(CarCategoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetByIdCommandHandler");

        try
        {
            var dbCarCategory = await _carCategoryRepository.GetByIdAsync(request.CarCategoryId, cancellationToken);

            if (dbCarCategory == null ||dbCarCategory is null)
            {
                _logger.LogWarning("CarCategoryGetByIdCommandHandler: CarCategory doesn't exist!");
                return Result.Failure<CarCategoryDetailDto?>(new Error(
                    "CarCategory.NotFound",
                    $"The CarCategory with Id {request.CarCategoryId} was not found"));

            }

            var resultDto = _mapper.Map<CarCategory, CarCategoryDetailDto>(dbCarCategory);

            _logger.LogInformation("Finished CarCategoryGetByIdCommandHandler");
            return resultDto;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarCategoryDetailDto?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
