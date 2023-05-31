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

namespace Application.CarBrands.Update;

internal sealed class CarBrandUpdateCommandHandler : IQueryHandler<CarBrandUpdateCommand, bool>
{
    private ILogger<CarBrandUpdateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandUpdateCommandHandler(ILogger<CarBrandUpdateCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(CarBrandUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandUpdateCommandHandler");

        try
        {
            var dbCarBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            if (dbCarBrand == null)
            {

                _logger.LogWarning("CarBrandUpdateCommandHandler: CarBrand doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarBrand.NotFound",
                    $"The CarBrand with Id {request.CarBrandId} was not found"));
            }

            dbCarBrand.Update(request.CarBrandName);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarBrandUpdateCommandHandler");
            return true;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
