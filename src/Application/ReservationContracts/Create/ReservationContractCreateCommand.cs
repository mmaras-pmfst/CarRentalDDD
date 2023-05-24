using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Create;

public sealed record ReservationContractCreateCommand(
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickupLocationId,
    Guid DropDownLocationId) : IRequest<Unit>;
