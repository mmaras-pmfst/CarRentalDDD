
namespace WebApi.Contracts.Reservations;

public record CreateReservationRequest(
    string DriverFirstName,
    string DriverLastName,
    string Email,
    DateTime PickUpDate,
    DateTime DropDownDate,
    Guid PickUpLocationId,
    Guid DropDownLocationId,
    Guid CarModelId,
    List<ExtrasRequest>? Extras);

public record ExtrasRequest(
    Guid ExtraId,
    int Quantity);