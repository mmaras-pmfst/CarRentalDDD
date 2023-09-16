using Application.Abstractions;
using Domain.Management.CarBrands;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetAll;

internal sealed class CarBrandGetAllQueryHandler : IQueryHandler<CarBrandGetAllQuery, List<CarBrand>>
{
    private ILogger<CarBrandGetAllQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandGetAllQueryHandler(ILogger<CarBrandGetAllQueryHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<CarBrand>>> Handle(CarBrandGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetAllCommandHandler");

        try
        {
            var dbCarBrands = await _carBrandRepository.GetAllAsync(cancellationToken);

            if (!dbCarBrands.Any())
            {
                _logger.LogWarning("CarBrandGetAllCommandHandler: No CarBrands in database");
                return Result.Failure<List<CarBrand>>(new Error(
                        "CarBrand.NoData",
                        "There are no CarBrands to fetch"));
            }

            //TODO: make mapping if needed!!!

            _logger.LogInformation("Finished CarBrandGetAllCommandHandler");
            return dbCarBrands;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetAllCommandHandler error: {0}", ex.Message);
            return Result.Failure<List<CarBrand>>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
