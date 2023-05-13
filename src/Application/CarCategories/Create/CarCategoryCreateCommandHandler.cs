using Domain.CarCategory;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.Create;

internal sealed class CarCategoryCreateCommandHandler : IRequestHandler<CarCategoryCreateCommand, Unit>
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

    public async Task<Unit> Handle(CarCategoryCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryCreateCommandHandler");

        try
        {
            var exists = await _carCategoryRepository.AlreadyExists(request.shortName, cancellationToken);
            if (exists)
            {
                _logger.LogWarning("CarCategoryCreateCommandHandler: CarCategory already exists!");

                return Unit.Value;
            }

            var newCarCategory = CarCategory.Create(
                Guid.NewGuid(),
                request.name,
                request.shortName,
                request.description);

            await _carCategoryRepository.AddAsync(newCarCategory,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarCategoryCreateCommandHandler");

            return Unit.Value;

        }
        catch (Exception ex)
        {
            _logger.LogError("CarCategoryCreateCommandHandler error: {0}", ex.Message);

            throw;
        }

    }
}
