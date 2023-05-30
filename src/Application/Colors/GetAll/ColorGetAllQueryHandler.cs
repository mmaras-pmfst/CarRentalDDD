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

namespace Application.Colors.GetAll
{
    internal sealed class ColorGetAllQueryHandler : IQueryHandler<ColorGetAllQuery, List<Color>>
    {
        private ILogger<ColorGetAllQueryHandler> _logger;
        private readonly IColorRepository _colorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColorGetAllQueryHandler(ILogger<ColorGetAllQueryHandler> logger, IColorRepository colorRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _colorRepository = colorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<Color>>> Handle(ColorGetAllQuery request, CancellationToken cancellationToken)
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
