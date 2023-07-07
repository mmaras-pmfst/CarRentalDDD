using Application.Abstractions;
using Domain.Repositories;
using Domain.Sales.CarModelRent.Entities;
using Domain.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;
internal class ReservationGetAllQueryHandler : IQueryHandler<ReservationGetAllQuery, List<Reservation>>
{
    private ILogger<ReservationGetAllQueryHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;

    public ReservationGetAllQueryHandler(ILogger<ReservationGetAllQueryHandler> logger, ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<Result<List<Reservation>>> Handle(ReservationGetAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetAllQueryHandler");

        try
        {
            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);

            var reservations = brands
                .SelectMany(x => x.CarModels)
                .SelectMany(x => x.CarModelRents)
                .SelectMany(x => x.Reservations)
                .Where(x => x.PickUpDate.Date >= request.DateFrom.Date && x.PickUpDate.Date <= request.DateTo.Date)
                .ToList();

            if (!reservations.Any())
            {
                //return no data to fetch
            }
            _logger.LogInformation("Finished ReservationGetAllQueryHandler");
            return reservations;

        }
        catch (Exception ex)
        {
            _logger.LogError("ReservationGetAllQueryHandler error: {0}", ex.Message);
            return Result.Failure<List<Reservation>>(new Error(
                    "Error",
                    ex.Message));
        }
    }
}
