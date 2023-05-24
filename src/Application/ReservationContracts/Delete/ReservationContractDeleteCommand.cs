using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Delete;

public sealed record ReservationContractDeleteCommand(Guid ReservationId) : IRequest<Unit>;