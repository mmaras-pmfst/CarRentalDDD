using Domain.Sales.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IContractItemRepository
{
    Task AddAsync(ContractItem contractItem, CancellationToken cancellationToken = default);

}
