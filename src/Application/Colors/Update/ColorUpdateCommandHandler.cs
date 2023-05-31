using Application.Abstractions;
using Domain.Color;
using Domain.Repositories;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Update
{
    internal sealed class ColorUpdateCommandHandler : ICommandHandler<ColorUpdateCommand, bool>
    {
        private ILogger<ColorUpdateCommandHandler> _logger;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorUpdateCommandHandler(ILogger<ColorUpdateCommandHandler> logger, IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(ColorUpdateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorUpdateCommandHandler");

            try
            {
                var dbColor = await _colorRepository.GetByIdAsync(request.ColorId, cancellationToken);
                if (dbColor == null)
                {
                    _logger.LogWarning("ColorUpdateCommandHandler: Color doesn't exist!");
                    return Result.Failure<bool>(new Error(
                        "Color.NotFound",
                        $"The Color with Id {request.ColorId} was not found"));
                }

                dbColor.Update(request.ColorName);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Finished ColorUpdateCommandHandler");
                return true;

            }
            catch (Exception ex)
            {

                _logger.LogError("ColorUpdateCommandHandler error: {0}", ex.Message);
                return Result.Failure<bool>(new Error(
                    "Error",
                    ex.Message));
            }

        }
    }
}
