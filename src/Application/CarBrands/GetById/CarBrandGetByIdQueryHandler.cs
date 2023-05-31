using Application.Abstractions;
using Domain.CarBrand;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetById;

internal sealed class CarBrandGetByIdQueryHandler : IQueryHandler<CarBrandGetByIdQuery, CarBrand?>
{
    private ILogger<CarBrandGetByIdQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandGetByIdQueryHandler(ILogger<CarBrandGetByIdQueryHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CarBrand?>> Handle(CarBrandGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetByIdCommandHandler");

        try
        {
            var dbCarBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            if (dbCarBrand == null)
            {

                _logger.LogWarning("CarBrandGetByIdCommandHandler: CarBrand doesn't exist!");
                return Result.Failure<CarBrand?>(new Error(
                    "CarBrand.NotFound",
                    $"The CarBrand with Id {request.CarBrandId} was not found"));
            }

            //TODO: make mapping if needed!!!

            _logger.LogInformation("Finished CarBrandGetByIdCommandHandler");
            return dbCarBrand;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetByIdCommandHandler error: {0}", ex.Message);
            return Result.Failure<CarBrand?>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
