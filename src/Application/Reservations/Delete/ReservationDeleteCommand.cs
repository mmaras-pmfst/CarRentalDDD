using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Delete;

public sealed record ReservationDeleteCommand(Guid ReservationId) : IRequest<Unit>;