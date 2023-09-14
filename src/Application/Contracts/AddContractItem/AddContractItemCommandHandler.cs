using Application.Abstractions;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.AddContractItem;
internal class AddContractItemCommandHandler : ICommandHandler<AddContractItemCommand, Guid>
{
    public Task<Result<Guid>> Handle(AddContractItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
