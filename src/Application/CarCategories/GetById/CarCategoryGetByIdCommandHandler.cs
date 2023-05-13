using Domain.CarCategory;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetById;

internal sealed class CarCategoryGetByIdCommandHandler : IRequestHandler<CarCategoryGetByIdCommand, CarCategory?>
{
    private ILogger<CarCategoryGetByIdCommandHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCategoryGetByIdCommandHandler(ILogger<CarCategoryGetByIdCommandHandler> logger, ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CarCategory?> Handle(CarCategoryGetByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetByIdCommandHandler");

        try
        {
            var dbCarCategory = await _carCategoryRepository.GetByIdAsync(request.id, cancellationToken);

            if (dbCarCategory == null)
            {
                _logger.LogWarning("CarCategoryGetByIdCommandHandler: CarCategory doesn't exist!");
                return null;

            }

            //TODO: mapping if needed!!!

            _logger.LogInformation("Finished CarCategoryGetByIdCommandHandler");
            return dbCarCategory;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryGetByIdCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
