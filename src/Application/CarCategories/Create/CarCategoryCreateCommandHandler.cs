using Application.Abstractions;
using Domain.Management.CarCategory;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Management.CarCategory.ValueObjects;
using Domain.Common.ValueObjects;

namespace Application.CarCategories.Create;

internal sealed class CarCategoryCreateCommandHandler : ICommandHandler<CarCategoryCreateCommand, Guid>
{
    private ILogger<CarCategoryCreateCommandHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCategoryCreateCommandHandler(ILogger<CarCategoryCreateCommandHandler> logger, ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CarCategoryCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryCreateCommandHandler");

        try
        {
            var exists = await _carCategoryRepository.AlreadyExists(request.ShortName, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("CarCategoryCreateCommandHandler: CarCategory already exists!");
                return Result.Failure<Guid>(DomainErrors.CarCategory.CarCategoryAlreadyExists);

            }

            var carCategoryNameResult = CarCategoryName.Create(request.Name);
            if (carCategoryNameResult.IsFailure)
            {
                return Result.Failure<Guid>(carCategoryNameResult.Error);
            }
            var carCategoryShortNameResult = CarCategoryShortName.Create(request.ShortName);
            if (carCategoryShortNameResult.IsFailure)
            {
                return Result.Failure<Guid>(carCategoryShortNameResult.Error);

            }
            var descriptionResult = Description.Create(request.Description);
            if (descriptionResult.IsFailure)
            {
                return Result.Failure<Guid>(descriptionResult.Error);

            }

            var newCarCategory = CarCategory.Create(
                Guid.NewGuid(),
                carCategoryNameResult.Value,
                carCategoryShortNameResult.Value,
                descriptionResult.Value);

            await _carCategoryRepository.AddAsync(newCarCategory,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarCategoryCreateCommandHandler");

            return newCarCategory.Id;

        }
        catch (Exception ex)
        {
            _logger.LogError("CarCategoryCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
