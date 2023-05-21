using Domain.CarBrand.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;

internal sealed class ReservationGetAllCommandHandler : IRequestHandler<ReservationGetAllCommand, List<Reservation>>
{
    private ILogger<ReservationGetAllCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;

    public ReservationGetAllCommandHandler(ILogger<ReservationGetAllCommandHandler> logger, ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<List<Reservation>> Handle(ReservationGetAllCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetAllCommandHandler");

        try
        {
            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);
            var reservations = brands.SelectMany(x => x.CarModels).SelectMany(x => x.Reservations).ToList();

            _logger.LogInformation("Finished ReservationGetAllCommandHandler");
            return reservations;
        }
        catch (Exception ex)
        {

            _logger.LogError("ReservationGetAllCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
