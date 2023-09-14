using Application.Abstractions;
using Domain.Errors;
using Domain.Management.CarBrands.ValueObjects;
using Domain.Management.CarModels.ValueObjects;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Create;

internal sealed class CarModelCreateCommandHandler : ICommandHandler<CarModelCreateCommand, Guid>
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

    public async Task<Result<Guid>> Handle(CarModelCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelCreateCommandHandler");

        try
        {
            var carCategory = await _carCategoryRepository
                .GetByIdAsync(request.CarCategoryId, cancellationToken);

            var carBrand = await _carBrandRepository
                .GetByIdAsync(request.CarBrandId, cancellationToken);


            if (carCategory is null)
            {
                _logger.LogWarning("CarModelCreateCommandHandler: CarCategory doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "CarCategory.NotFound",
                    $"The CarCategory with Id {request.CarCategoryId} was not found"));
            }

            if(carBrand is null)
            {
                _logger.LogWarning("CarModelCreateCommandHandler: CarBrand doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "CarBrand.NotFound",
                    $"The CarBrand with Id {request.CarBrandId} was not found"));
            }

            var carModelExist = carBrand.CarModels.Where(x => x.Name.Value == request.CarModelName).Any();
            if (carModelExist)
            {
                return Result.Failure<Guid>(DomainErrors.CarModel.CarModelAlreadyExists);

            }
            var carModelNameResult = CarModelName.Create(request.CarModelName);
            if (carModelNameResult.IsFailure)
            {
                return Result.Failure<Guid>(carModelNameResult.Error);
            }
            var carModel = carBrand.CreateCarModel(carModelNameResult.Value, carCategory);

            await _carModelRepository.AddAsync(carModel, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CarModelCreateCommandHandler");
            return carModel.Id;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
