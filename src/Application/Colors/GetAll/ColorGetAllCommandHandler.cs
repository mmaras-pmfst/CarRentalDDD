using Domain.Color;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetAll
{
    internal sealed class ColorGetAllCommandHandler : IRequestHandler<ColorGetAllCommand, List<Color>>
    {
        private ILogger<ColorGetAllCommandHandler> _logger;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorGetAllCommandHandler(ILogger<ColorGetAllCommandHandler> logger, IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Color>> Handle(ColorGetAllCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started ColorGetAllCommandHandler");

            try
            {
                var dbColors = await _colorRepository.GetAllAsync(cancellationToken);

                //TODO: make mapping if needed!!!

                _logger.LogInformation("Finished ColorGetAllCommandHandler");
                return dbColors;
            }
            catch (Exception ex)
            {
                _logger.LogError("ColorGetAllCommandHandler error: {0}", ex.Message);

                throw;
            }
        }
    }
}
