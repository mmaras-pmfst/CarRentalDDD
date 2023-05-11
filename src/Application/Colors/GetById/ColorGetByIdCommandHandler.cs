using Domain.Color;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetById
{
    internal sealed class ColorGetByIdCommandHandler : IRequestHandler<ColorGetByIdCommand, Color?>
    {
        private ILogger<ColorGetByIdCommandHandler> _logger;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorGetByIdCommandHandler(ILogger<ColorGetByIdCommandHandler> logger, IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Color?> Handle(ColorGetByIdCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorGetByIdCommandHandler");

            try
            {
                var dbColor = await _colorRepository.GetByIdAsync(request.id, cancellationToken);
                if (dbColor == null)
                {
                    _logger.LogWarning("ColorGetByIdCommandHandler: Color doesn't exist!");
                    return null;
                }

                //TODO: make mapping if needed!!!

                _logger.LogInformation("Finished ColorGetByIdCommandHandler");
                return dbColor;
            }
            catch (Exception ex)
            {
                _logger.LogError("ColorGetByIdCommandHandler error: {0}", ex.Message);

                throw;
            }
        }
    }
}
