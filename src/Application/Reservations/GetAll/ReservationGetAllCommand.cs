using Domain.CarBrand.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.GetAll;

public sealed record ReservationGetAllCommand() : IRequest<List<Reservation>>;