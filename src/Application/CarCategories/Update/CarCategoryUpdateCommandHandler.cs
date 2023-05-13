using Domain.CarCategory;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.Update;

internal sealed class CarCategoryUpdateCommandHandler : IRequestHandler<CarCategoryUpdateCommand, Unit>
{
    private ILogger<CarCategoryUpdateCommandHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCategoryUpdateCommandHandler(ILogger<CarCategoryUpdateCommandHandler> logger, ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(CarCategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryUpdateCommandHandler");

        try
        {
            var dbCarCategory = await _carCategoryRepository.GetByIdAsync(request.id, cancellationToken);
            if (dbCarCategory == null)
            {
                _logger.LogWarning("CarCategoryUpdateCommandHandler: CarCategory doesn't exist!");
                return Unit.Value;
            }

            dbCarCategory = CarCategory.Update(
                request.id,
                request.name,
                request.shortName,
                request.description);


            await _carCategoryRepository.Update(dbCarCategory, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarCategoryUpdateCommandHandler");
            return Unit.Value;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryUpdateCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
