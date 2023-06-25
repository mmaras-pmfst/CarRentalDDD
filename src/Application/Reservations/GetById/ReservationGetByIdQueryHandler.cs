using Application.Abstractions;
using Domain.Sales.CarModelRent.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetById;
internal class ReservationGetByIdQueryHandler : IQueryHandler<ReservationGetByIdQuery, Reservation>
{
    public Task<Result<Reservation>> Handle(ReservationGetByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
