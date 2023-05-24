using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Update;

public sealed record ReservationContractUpdateCommand(
    Guid ReservationContractId,
    decimal Price) : IRequest<Unit>;