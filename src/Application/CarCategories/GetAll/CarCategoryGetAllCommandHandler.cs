using Domain.CarCategory;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetAll;

internal sealed class CarCategoryGetAllCommandHandler : IRequestHandler<CarCategoryGetAllCommand, List<CarCategory>>
{
    private ILogger<CarCategoryGetAllCommandHandler> _logger;
    private readonly ICarCategoryRepository _carCategoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CarCategoryGetAllCommandHandler(ILogger<CarCategoryGetAllCommandHandler> logger, ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _carCategoryRepository = carCategoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CarCategory>> Handle(CarCategoryGetAllCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryGetAllCommandHandler");

        try
        {
            var dbCarCategories = await _carCategoryRepository.GetAllAsync(cancellationToken);

            //TODO: make mapping if needed!!!
            _logger.LogInformation("Finished CarCategoryGetAllCommandHandler");
            return dbCarCategories;

        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryGetAllCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
