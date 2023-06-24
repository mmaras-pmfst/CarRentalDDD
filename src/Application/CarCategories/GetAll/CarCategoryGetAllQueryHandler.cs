using Application.Abstractions;
using Domain.Management.CarCategory;
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

internal sealed class CarCategoryGetAllQueryHandler : IQueryHandler<CarCategoryGetAllQuery, List<CarCategory>>
{
    private ILogger<CarCategoryGetAllQueryHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCategoryGetAllQueryHandler(ILogger<CarCategoryGetAllQueryHandler> logger, ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<CarCategory>>> Handle(CarCategoryGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetAllCommandHandler");

        try
        {
            var dbCarCategories = await _carCategoryRepository.GetAllAsync(cancellationToken);

            if (!dbCarCategories.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarCategories in database");
                return Result.Failure<List<CarCategory>>(new Error(
                        "CarCategory.NoData",
                        "There are no CarCategories to fetch"));
            }

            //TODO: make mapping if needed!!!
            _logger.LogInformation("Finished CarCategoryGetAllCommandHandler");
            return dbCarCategories;

        }
        catch (Exception ex)
        {
            _logger.LogError("CarCategoryGetAllCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarCategory>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
