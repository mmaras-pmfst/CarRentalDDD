using Application.Abstractions;
using Domain.Management.Color;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetById
{
    internal sealed class ColorGetByIdQueryHandler : IQueryHandler<ColorGetByIdQuery, Color?>
    {
        private ILogger<ColorGetByIdQueryHandler> _logger;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorGetByIdQueryHandler(ILogger<ColorGetByIdQueryHandler> logger, IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Color?>> Handle(ColorGetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorGetByIdCommandHandler");

            try
            {
                var dbColor = await _colorRepository.GetByIdAsync(request.ColorId, cancellationToken);
                if (dbColor == null)
                {
                    _logger.LogWarning("ColorGetByIdCommandHandler: Color doesn't exist!");
                    return Result.Failure<Color?>(new Error(
                    "Color.NotFound",
                    $"The Color with Id {request.ColorId} was not found"));
                }

                //TODO: make mapping if needed!!!

                _logger.LogInformation("Finished ColorGetByIdCommandHandler");
                return dbColor;
            }
            catch (Exception ex)
            {
                _logger.LogError("ColorGetByIdCommandHandler error: {0}", ex.Message);
                return Result.Failure<Color?>(new Error(
                    "Error",
                    ex.Message));
            }
        
        }
    }
}
