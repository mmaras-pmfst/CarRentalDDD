using Application.Abstractions;
using Domain.CarCategory;
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

            var newCarCategory = CarCategory.Create(
                Guid.NewGuid(),
                request.Name,
                request.ShortName,
                request.Description);

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
