using Application.Abstractions;
using Domain.Sales.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;
public sealed record ReservationGetAllQuery(
    DateTime? DateFrom,
    DateTime? DateTo) : IQuery<List<Reservation>>;