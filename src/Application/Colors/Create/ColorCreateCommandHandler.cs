﻿using Domain.Color;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Create
{
    internal sealed class ColorCreateCommandHandler : IRequestHandler<ColorCreateCommand, Unit>
    {
        private ILogger<ColorCreateCommandHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IColorRepository _colorRepository;

        public ColorCreateCommandHandler(ILogger<ColorCreateCommandHandler> logger, IUnitOfWork unitOfWork, IColorRepository colorRepository)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _colorRepository = colorRepository;
        }

        public async Task<Unit> Handle(ColorCreateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorCreateCommandHandler");

            try
            {
                var exists = await _colorRepository.AlreadyExists(request.colorName, cancellationToken);
                if (exists)
                {
                    _logger.LogWarning("ColorCreateCommandHandler: Color already exists!");
                    return Unit.Value;
                }
                var newColor = Color.Create(
                    Guid.NewGuid(),
                    request.colorName);

                await _colorRepository.AddAsync(newColor, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Finished ColorCreateCommandHandler");
                return Unit.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError("ColorCreateCommandHandler error: {0}", ex.Message);

                throw;
            }
        }
    }
}
