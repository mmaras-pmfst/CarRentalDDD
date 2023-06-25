using Application.Abstractions;
using Domain.Repositories;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModelRents.Create;
internal class CarModelRentCreateCommandHandler : ICommandHandler<CarModelRentCreateCommand, Guid>
{
    private ILogger<CarModelRentCreateCommandHandler> _logger;
    private readonly ICarModelRentRepository _carModelRentRepository;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarModelRentCreateCommandHandler(ILogger<CarModelRentCreateCommandHandler> logger, ICarModelRentRepository carModelRentRepository, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carModelRentRepository = carModelRentRepository;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CarModelRentCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelRentCreateCommandHandler");

        try
        {
            var carBrand = await _carBrandRepository.GetAllAsync(cancellationToken);
            if (carBrand == null)
            {
                _logger.LogWarning("CarModelRentCreateCommandHandler: No CarBrands in database");
                return Result.Failure<Guid>(new Error(
                        "CarBrand.NoData",
                        "There are no CarBrands to fetch"));
            }
            var carModel = carBrand
                            .SelectMany(x => x.CarModels)
                            .Where(x => x.Id == request.CarModelId)
                            .SingleOrDefault();

            if(carModel == null)
            {
                _logger.LogWarning("CarModelRentCreateCommandHandler: CarModel doesn't exist!");
                return Result.Failure<Guid>(new Error(
                    "CarModel.NotFound",
                    $"The CarModel with Id {request.CarModelId} was not found"));
            }

            var carModelRent = carModel.AddCarModelRent(
                Guid.NewGuid(),
                request.ValidFrom,
                request.ValidUntil,
                request.PricePerDay,
                request.Discount,
                request.IsVisible);

            await _carModelRentRepository.AddAsync(carModelRent, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarModelRentCreateCommandHandler");

            return carModelRent.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("CarModelRentCreateCommandHandler error: {0}", ex.Message);
            return Result.Failure<Guid>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
