using Domain.CarBrand.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.GetAll;

internal sealed class ReservationContractGetAllCommandHandler : IRequestHandler<ReservationContractGetAllCommand, List<ReservationContract>>
{
    private ILogger<ReservationContractGetAllCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;

    public ReservationContractGetAllCommandHandler(ILogger<ReservationContractGetAllCommandHandler> logger, ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<List<ReservationContract>> Handle(ReservationContractGetAllCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetAllCommandHandler");

        try
        {
            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);
            var reservations = brands.SelectMany(x => x.CarModels).SelectMany(x => x.ReservationContracts).ToList();

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
