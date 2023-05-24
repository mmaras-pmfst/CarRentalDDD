using Domain.CarBrand.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.GetById;

public sealed record ReservationContractGetByIdCommand(Guid ReservationId) : IRequest<ReservationContract>;