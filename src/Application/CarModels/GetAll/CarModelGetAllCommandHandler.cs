using Domain.CarBrand.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetAll;

internal sealed class CarModelGetAllCommandHandler : IRequestHandler<CarModelGetAllCommand, List<CarModel>>
{
    private ILogger<CarModelGetAllCommandHandler> _logger;
    private ICarBrandRepository _carBrandRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelGetAllCommandHandler(
        ILogger<CarModelGetAllCommandHandler> logger,
        ICarBrandRepository carBrandRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<List<CarModel>> Handle(CarModelGetAllCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelGetAllCommandHandler");

        try
        {
            var carBrands = await _carBrandRepository.GetAllAsync(cancellationToken);

            var carModels = carBrands.SelectMany(x => x.CarModels).ToList();

            _logger.LogInformation("Finished CarModelGetAllCommandHandler");

            return carModels;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelCreateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
