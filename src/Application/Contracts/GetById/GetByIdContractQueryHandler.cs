using Application.Abstractions;
using Domain.Sales.Contracts;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.GetById;
internal class GetByIdContractQueryHandler : IQueryHandler<GetByIdContractQuery, Contract?>
{
    public Task<Result<Contract?>> Handle(GetByIdContractQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
