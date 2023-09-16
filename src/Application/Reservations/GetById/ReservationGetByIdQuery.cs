using Application.Abstractions;
using Domain.Sales.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetById;
public sealed record ReservationGetByIdQuery(
    Guid ReservationId) : IQuery<Reservation?>;