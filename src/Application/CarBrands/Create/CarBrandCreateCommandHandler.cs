using Domain.CarBrand;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.Create;

internal sealed class CarBrandCreateCommandHandler : IRequestHandler<CarBrandCreateCommand, Unit>
{
    private ILogger<CarBrandCreateCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandCreateCommandHandler(ILogger<CarBrandCreateCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CarBrandCreateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandCreateCommandHandler");

        try
        {
            var exists = await _carBrandRepository.AlreadyExists(request.carBrandName, cancellationToken);
            if (exists)
            {

                _logger.LogWarning("CarBrandCreateCommandHandler: CarBrand already exists!");
                return Unit.Value;
            }

            var newCarBrand = CarBrand.Create(
                Guid.NewGuid(),
                request.carBrandName);

            await _carBrandRepository.AddAsync(newCarBrand, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarBrandCreateCommandHandler");
            return Unit.Value;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandCreateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
