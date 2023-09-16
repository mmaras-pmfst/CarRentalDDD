using Application.Abstractions;
using Application.Reservations.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.RemoveReservationItem;
public sealed record RemoveReservationItemCommand(
    Guid ReservationId,
    List<Guid> ExtraIds) : ICommand<Guid>;