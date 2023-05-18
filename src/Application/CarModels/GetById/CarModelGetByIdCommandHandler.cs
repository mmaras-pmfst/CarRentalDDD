using Domain.CarBrand.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetById;

internal sealed class CarModelGetByIdCommandHandler : IRequestHandler<CarModelGetByIdCommand, CarModel?>
{
    private ILogger<CarModelGetByIdCommandHandler> _logger;
    private ICarBrandRepository _carBrandRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelGetByIdCommandHandler(
        ILogger<CarModelGetByIdCommandHandler> logger,
        ICarBrandRepository carBrandRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CarModel?> Handle(CarModelGetByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetByIdCommandHandler");

        try
        {
            var carBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            if(carBrand is null)
            {
                return null;
            }

            var carModel = carBrand.CarModels.FirstOrDefault(x => x.Id == request.CarModelId);

            if(carModel is null)
            {
                return null;
            }

            _logger.LogInformation("Finished CarModelGetByIdCommandHandler");

            return carModel;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelGetByIdCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
