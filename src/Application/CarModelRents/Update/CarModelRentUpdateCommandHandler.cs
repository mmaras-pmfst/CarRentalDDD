using Application.Abstractions;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModelRents.Update;
internal class CarModelRentUpdateCommandHandler : ICommandHandler<CarModelRentUpdateCommand, bool>
{
    private ILogger<CarModelRentUpdateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarModelRentUpdateCommandHandler(ILogger<CarModelRentUpdateCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CarModelRentUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelRentUpdateCommandHandler");

        try
        {
            var carBrand = await _carBrandRepository.GetAllAsync(cancellationToken);

            if (carBrand == null)
            {
                _logger.LogWarning("CarModelRentUpdateCommandHandler: No CarBrands in database");
                return Result.Failure<bool>(new Error(
                        "CarBrand.NoData",
                        "There are no CarBrands to fetch"));
            }

            var carModel = carBrand
                             .SelectMany(x => x.CarModels)
                             .Where(x => x.Id == request.CarModelId)
                             .SingleOrDefault();

            if (carModel == null)
            {
                _logger.LogWarning("CarModelRentUpdateCommandHandler: CarModel doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var response = carModel.UpdateCarModelRent(request.CarModelRentId, request.ValidFrom, request.ValidUntil, request.PricePerDay, request.Discount, request.IsVisible);

            if (response.IsFailure)
            {
                return Result.Failure<bool>(new Error(
                    response.Error.Code,
                    response.Error.Message));
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarModelRentUpdateCommandHandler");
            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError("CarModelRentUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
