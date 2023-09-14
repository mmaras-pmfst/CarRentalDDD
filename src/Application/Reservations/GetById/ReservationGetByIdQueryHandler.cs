using Application.Abstractions;
using Domain.Repositories;
using Domain.Sales.Reservations;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetById;
internal class ReservationGetByIdQueryHandler : IQueryHandler<ReservationGetByIdQuery, Reservation>
{
    private ILogger<ReservationGetByIdQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;

    public ReservationGetByIdQueryHandler(ILogger<ReservationGetByIdQueryHandler> logger, ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<Result<Reservation>> Handle(ReservationGetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetByCustomerQueryHandler");

        try
        {
            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);
            var reservation = brands.SelectMany(x => x.CarModels).SelectMany(x => x.CarModelRents).SelectMany(x => x.Reservations)
                .Where(x => x.Id == request.ReservationId).SingleOrDefault();

            if (reservation == null)
            {
                //return doesnt exists
            }

            _logger.LogInformation("Finished ReservationGetByCustomerQueryHandler");
            return reservation;
        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationGetByCustomerQueryHandler error: {0}", ex.Message);
            return Result.Failure<Reservation>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
