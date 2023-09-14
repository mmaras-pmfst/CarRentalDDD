using Domain.Sales.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
}
