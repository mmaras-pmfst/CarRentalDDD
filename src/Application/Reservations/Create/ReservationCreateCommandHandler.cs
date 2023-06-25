using Application.Abstractions;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Create;
internal class ReservationCreateCommandHandler : ICommandHandler<ReservationCreateCommand, Guid>
{
    public Task<Result<Guid>> Handle(ReservationCreateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
