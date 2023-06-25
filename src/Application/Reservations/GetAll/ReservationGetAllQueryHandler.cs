using Application.Abstractions;
using Domain.Sales.CarModelRent.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;
internal class ReservationGetAllQueryHandler : IQueryHandler<ReservationGetAllQuery, List<Reservation>>
{
    public Task<Result<List<Reservation>>> Handle(ReservationGetAllQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
