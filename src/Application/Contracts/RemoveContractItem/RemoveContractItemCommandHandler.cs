using Application.Abstractions;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.RemoveContractItem;
internal class RemoveContractItemCommandHandler : ICommandHandler<RemoveContractItemCommand, Guid>
{
    public Task<Result<Guid>> Handle(RemoveContractItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
