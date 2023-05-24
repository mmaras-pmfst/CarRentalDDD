using Domain.CarBrand;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Update;

internal sealed class CarModelUpdateCommandHandler : IRequestHandler<CarModelUpdateCommand, Unit>
{
    private ILogger<CarModelUpdateCommandHandler> _logger;
    private ICarModelRepository _carModelRepository;
    private ICarBrandRepository _carBrandRepository;
    private ICarCategoryRepository _carCategoryRepository;
    private IUnitOfWork _unitOfWork;

    public CarModelUpdateCommandHandler(
        ILogger<CarModelUpdateCommandHandler> logger,
        ICarModelRepository carModelRepository,
        ICarBrandRepository carBrandRepository,
        ICarCategoryRepository carCategoryRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carModelRepository = carModelRepository;
        _carBrandRepository = carBrandRepository;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CarModelUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarModelUpdateCommandHandler");

        try
        {
            var carBrand = await _carBrandRepository.GetByIdAsync(request.CarBrandId, cancellationToken);
            var carCategory = await _carCategoryRepository.GetByIdAsync(request.CarCategoryId, cancellationToken);
            if (carBrand is null || carCategory is null)
            {
                return Unit.Value;
            }

            var carModel = carBrand.CarModels.FirstOrDefault(x => x.Id == request.CarModelId);

            if (carModel is null)
            {
                return Unit.Value;
            }


            carModel.Update(request.CarModelName, request.BasePricePerDay, carCategory);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Finished CarModelUpdateCommandHandler");
            return Unit.Value;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarModelUpdateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
