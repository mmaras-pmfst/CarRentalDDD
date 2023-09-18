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

namespace Application.CarCategories.GetAll;

internal sealed class CarCategoryGetAllQueryHandler : IQueryHandler<CarCategoryGetAllQuery, List<CarCategoryDto>>
{
    private ILogger<CarCategoryGetAllQueryHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private IMapper _mapper;


    public CarCategoryGetAllQueryHandler(ILogger<CarCategoryGetAllQueryHandler> logger, ICarCategoryRepository carCategoryRepository, IMapper mapper)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CarCategoryDto>>> Handle(CarCategoryGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetAllCommandHandler");

        try
        {
            var dbCarCategories = await _carCategoryRepository.GetAllAsync(cancellationToken);

            if (!dbCarCategories.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarCategories in database");
                return Result.Failure<List<CarCategoryDto>>(new Error(
                        "CarCategory.NoData",
                        "There are no CarCategories to fetch"));
            }

            var resultDto = _mapper.Map<List<CarCategory>,List<CarCategoryDto>>(dbCarCategories);
            _logger.LogInformation("Finished CarCategoryGetAllCommandHandler");
            return resultDto;

        }
        catch (Exception ex)
        {
            _logger.LogError("CarCategoryGetAllCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarCategoryDto>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
