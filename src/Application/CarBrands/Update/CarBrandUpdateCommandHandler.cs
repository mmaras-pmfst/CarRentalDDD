using Domain.CarBrand;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.Update;

internal sealed class CarBrandUpdateCommandHandler : IRequestHandler<CarBrandUpdateCommand, Unit>
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

    public async Task<Unit> Handle(CarBrandUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandUpdateCommandHandler");

        try
        {
            var dbCarBrand = await _carBrandRepository.GetByIdAsync(request.id, cancellationToken);
            if (dbCarBrand == null)
            {

                _logger.LogWarning("CarBrandUpdateCommandHandler: CarBrand doesn't exist!");
                return Unit.Value;
            }

            dbCarBrand.Update(request.carBrandName);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarBrandUpdateCommandHandler");
            return Unit.Value;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandUpdateCommandHandler error: {0}", ex.Message);
            throw;
        }
    }
}
