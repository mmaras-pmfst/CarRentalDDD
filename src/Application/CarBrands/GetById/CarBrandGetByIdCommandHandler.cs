using Domain.CarBrand;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetById;

internal sealed class CarBrandGetByIdCommandHandler : IRequestHandler<CarBrandGetByIdCommand, CarBrand?>
{
    private ILogger<CarBrandGetByIdCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandGetByIdCommandHandler(ILogger<CarBrandGetByIdCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CarBrand?> Handle(CarBrandGetByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetByIdCommandHandler");

        try
        {
            var dbCarBrand = await _carBrandRepository.GetByIdAsync(request.id, cancellationToken);
            if (dbCarBrand == null)
            {

                _logger.LogWarning("CarBrandGetByIdCommandHandler: CarBrand doesn't exist!");
                return null;
            }

            //TODO: make mapping if needed!!!

            _logger.LogInformation("Finished CarBrandGetByIdCommandHandler");
            return dbCarBrand;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetByIdCommandHandler error: {0}", ex.Message);
            throw;
        }
    }
}
