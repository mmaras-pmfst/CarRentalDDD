using Application.Abstractions;
using Application.Reservations.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.AddReservationItem;
public sealed record AddReservationItemCommand(
    Guid ReservationId,
    List<ExtrasModel> Extras) : ICommand<Guid>;