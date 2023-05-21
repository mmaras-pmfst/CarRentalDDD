using Domain.CarBrand.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetById;

public sealed record ReservationGetByIdCommand(Guid ReservationId) : IRequest<Reservation>;