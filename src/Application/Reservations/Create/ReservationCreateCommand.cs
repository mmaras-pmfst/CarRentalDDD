using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Create;
public sealed record ReservationCreateCommand(
    string DriverFirstName,
    string DriverLastName,
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid PickUpLocationId,
    Guid DropDownLocationId,
    Guid CarModelId,
    List<ExtrasModel>? Extras) : ICommand<Guid>;


public sealed record ExtrasModel(
    Guid ExtraId,
    int Quantity);