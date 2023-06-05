using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.Create;

public sealed record ReservationContractCreateCommand(
    string DriverFirstName,
    string DriverLastName,
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid CarModelId,
    Guid PickUpLocationId,
    Guid DropDownLocationId) : ICommand<Guid>;