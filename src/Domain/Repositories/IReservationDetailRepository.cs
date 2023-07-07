using Domain.Sales.CarModelRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IReservationDetailRepository
{
    Task AddAsync(ReservationDetail reservationDetail, CancellationToken cancellationToken = default);
}
