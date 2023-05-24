using Domain.CarBrand.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.GetById;

internal sealed class ReservationContractGetByIdCommandHandler : IRequestHandler<ReservationContractGetByIdCommand, ReservationContract>
{
    private ILogger<ReservationContractGetByIdCommandHandler> _logger;
    private readonly ICarBrandRepository _carBrandRepository;

    public ReservationContractGetByIdCommandHandler(ILogger<ReservationContractGetByIdCommandHandler> logger, ICarBrandRepository carBrandRepository)
    {
        _logger = logger;
        _carBrandRepository = carBrandRepository;
    }

    public async Task<ReservationContract> Handle(ReservationContractGetByIdCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Started ReservationGetByIdCommandHandler");

        try
        {
            var brands = await _carBrandRepository.GetAllAsync(cancellationToken);

            var reservation = brands.SelectMany(x => x.CarModels).SelectMany(x => x.ReservationContracts).Where(x => x.Id == request.ReservationId).FirstOrDefault();
            if (reservation is null)
            {
                return null;
            }
            _logger.LogInformation("Finished ReservationGetByIdCommandHandler");
            return reservation;
        }
        catch (Exception ex)
        {

            _logger.LogError("ReservationGetByIdCommandHandler error: {0}", ex.Message);
            throw;
        }

    }
}
