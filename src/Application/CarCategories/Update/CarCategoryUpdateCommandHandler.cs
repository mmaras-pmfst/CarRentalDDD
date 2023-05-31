﻿using Application.Abstractions;
using Domain.CarCategory;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.Update;

internal sealed class CarCategoryUpdateCommandHandler : ICommandHandler<CarCategoryUpdateCommand, bool>
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

    public async Task<Result<bool>> Handle(CarCategoryUpdateCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started CarCategoryUpdateCommandHandler");

        try
        {
            var dbCarCategory = await _carCategoryRepository.GetByIdAsync(request.CarCategoryId, cancellationToken);
            if (dbCarCategory == null)
            {
                _logger.LogWarning("CarCategoryUpdateCommandHandler: CarCategory doesn't exist!");
                return Result.Failure<bool>(new Error(
                    "CarCategory.NotFound",
                    $"The CarCategory with Id {request.CarCategoryId} was not found"));
            }

            dbCarCategory.Update(request.Name, request.ShortName, request.Description);


            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Finished CarCategoryUpdateCommandHandler");
            return true;
        }
        catch (Exception ex)
        {

            _logger.LogError("CarCategoryUpdateCommandHandler error: {0}", ex.Message);
            return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
        }

    }
}
