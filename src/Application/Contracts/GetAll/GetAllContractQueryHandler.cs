using Application.Abstractions;
using Domain.Sales.Contracts;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.GetAll;
internal class GetAllContractQueryHandler : IQueryHandler<GetAllContractQuery, List<Contract>>
{
    public Task<Result<List<Contract>>> Handle(GetAllContractQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
