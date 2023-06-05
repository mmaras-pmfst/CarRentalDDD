using Application.Abstractions;
using Domain.ReservationContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ReservationContracts.GetById;
public sealed record ReservationContractGetByIdQuery(
    Guid ReservationContractId) : IQuery<ReservationContract>;