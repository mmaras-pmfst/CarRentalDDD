using Domain.CarBrand;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetAll;

internal sealed class CarBrandGetAllCommandHandler : IRequestHandler<CarBrandGetAllCommand, List<CarBrand>>
{
    private ILogger<CarBrandGetAllCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarBrandGetAllCommandHandler(ILogger<CarBrandGetAllCommandHandler> logger, ICarBrandRepository carBrandRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CarBrand>> Handle(CarBrandGetAllCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarBrandGetAllCommandHandler");

        try
        {
            var dbCarBrands = await _carBrandRepository.GetAllAsync(cancellationToken);

            //TODO: make mapping if needed!!!

            _logger.LogInformation("Finished CarBrandGetAllCommandHandler");
            return dbCarBrands;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarBrandGetAllCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
