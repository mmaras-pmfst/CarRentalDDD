using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Create;

internal sealed class CarModelCreateCommandHandler : IRequestHandler<CarModelCreateCommand, Unit>
{
    private ILogger<CarModelCreateCommandHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private ICarCategoryRepository _carCategoryRepository;
    private ICarBrandRepository _carBrandRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelCreateCommandHandler(
        ILogger<CarModelCreateCommandHandler> logger,
        ICarModelRepository carModelRepository,
        IUnitOfWork unitOfWork,
        ICarCategoryRepository carCategoryRepository,
        ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
        _unitOfWork = unitOfWork;
        _carCategoryRepository = carCategoryRepository;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<Unit> Handle(CarModelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelCreateCommandHandler");

        try
        {
            var carCategory = await _carCategoryRepository
                .GetByIdAsync(request.CarCategoryId, cancellationToken);

            var carBrand = await _carBrandRepository
                .GetByIdAsync(request.CarBrandId, cancellationToken);

            if (carCategory is null || carBrand is null)
            {
                return Unit.Value;
            }

            var carModel = carBrand.CreateCarModel(request.CarModelName, carCategory);

            await _carModelRepository.AddAsync(carModel, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CarModelCreateCommandHandler");
            return Unit.Value;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelCreateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
