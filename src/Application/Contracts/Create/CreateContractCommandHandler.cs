using Application.Abstractions;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Create;
internal class CreateContractCommandHandler : ICommandHandler<CreateContractCommand, Guid>
{
    public Task<Result<Guid>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
