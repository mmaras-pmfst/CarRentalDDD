using Domain.Repositories;
using Domain.Sales.Contracts.Entities;
using Domain.Sales.Reservations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ContractItemRepository : IContractItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ContractItemRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(ContractItem contractItem, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<ContractItem>().AddAsync(contractItem, cancellationToken);

    }
}
