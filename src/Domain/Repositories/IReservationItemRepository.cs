using Domain.Sales.Reservations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IReservationItemRepository
{
    Task AddAsync(ReservationItem reservationItem, CancellationToken cancellationToken = default);
}
