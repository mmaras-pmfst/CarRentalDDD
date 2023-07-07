using Application.Abstractions;
using Domain.Management.CarBrand;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Update;

internal sealed class CarModelUpdateCommandHandler : ICommandHandler<CarModelUpdateCommand, bool>
{
    private ILogger<CarModelUpdateCommandHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private ICarBrandRepository _carBrandRepository;
    private ICarCategoryRepository _carCategoryRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelUpdateCommandHandler(
        ILogger<CarModelUpdateCommandHandler> logger,
        ICarModelRepository carModelRepository,
        ICarBrandRepository carBrandRepository,
        ICarCategoryRepository carCategoryRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
        _carBrandRepository = carBrandRepository;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CarModelUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelUpdateCommandHandler");

        try
        {
            var carBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            var carCategory = await _carCategoryRepository.GetByIdAsync(request.CarCategoryId, cancellationToken);
            if (carBrand is null)
            {
                _logger.LogWarning("CarModelUpdateCommandHandler: CarBrand doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarBrand.NotFound",
                    $"The CarBrand with Id {request.CarBrandId} was not found"));
            }
            if(carCategory is null)
            {
                _logger.LogWarning("CarModelUpdateCommandHandler: CarCategory doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarCategory.NotFound",
                    $"The CarCategory with Id {request.CarCategoryId} was not found"));
            }

            var carModel = carBrand.CarModels.FirstOrDefault(x => x.Id == request.CarModelId);

            if (carModel is null)
            {
                _logger.LogWarning("CarModelUpdateCommandHandler: CarModel doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var carModelNameResult = CarModelName.Create(request.CarModelName);
            if (carModelNameResult.IsFailure)
            {
                return Result.Failure<bool>(carModelNameResult.Error);
            }
            carModel.Update(carModelNameResult.Value, carCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CarModelUpdateCommandHandler");
            return true;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
