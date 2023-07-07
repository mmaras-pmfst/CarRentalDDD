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

namespace Application.CarCategories.GetById;

internal sealed class CarCategoryGetByIdQueryHandler : IQueryHandler<CarCategoryGetByIdQuery, CarCategory?>
{
    private ILogger<CarCategoryGetByIdQueryHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;

    public CarCategoryGetByIdQueryHandler(ILogger<CarCategoryGetByIdQueryHandler> logger, ICarCategoryRepository carCategoryRepository)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
    }

    public async Task<Result<CarCategory?>> Handle(CarCategoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetByIdCommandHandler");

        try
        {
            var dbCarCategory = await _carCategoryRepository.GetByIdAsync(request.CarCategoryId, cancellationToken);

            if (dbCarCategory == null)
            {
                _logger.LogWarning("CarCategoryGetByIdCommandHandler: CarCategory doesn't exist!");
                return Result.Failure<CarCategory?>(new Error(
                    "CarCategory.NotFound",
                    $"The CarCategory with Id {request.CarCategoryId} was not found"));

            }

            //TODO: mapping if needed!!!

            _logger.LogInformation("Finished CarCategoryGetByIdCommandHandler");
            return dbCarCategory;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarCategory?>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
