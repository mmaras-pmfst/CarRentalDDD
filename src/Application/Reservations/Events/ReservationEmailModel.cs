using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Events;

public record ReservationEmailModel(
    decimal RentalPrice,
    decimal TotalPrice,
    string CarModelName,
    string CarBrandName);
