using Domain.CarBrand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IReservationContractRepository
{
    Task AddAsync(ReservationContract reservation, CancellationToken cancellationToken = default);
    Task Delete(Guid reservationId, CancellationToken cancellationToken = default);
}
