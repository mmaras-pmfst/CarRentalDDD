using Domain.Color;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Update
{
    internal sealed class ColorUpdateCommandHandler : IRequestHandler<ColorUpdateCommand, Unit>
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

        public async Task<Unit> Handle(ColorUpdateCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorUpdateCommandHandler");

            try
            {
                var dbColor = await _colorRepository.GetByIdAsync(request.id, cancellationToken);
                if (dbColor == null)
                {
                    _logger.LogWarning("ColorUpdateCommandHandler: Color doesn't exist!");
                    return Unit.Value;
                }

                dbColor = Color.Update(request.id, request.colorName);

                await _colorRepository.Update(dbColor, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Finished ColorUpdateCommandHandler");
                return Unit.Value;

            }
            catch (Exception ex)
            {

                _logger.LogError("ColorUpdateCommandHandler error: {0}", ex.Message);
                throw;
            }

        }
    }
}
