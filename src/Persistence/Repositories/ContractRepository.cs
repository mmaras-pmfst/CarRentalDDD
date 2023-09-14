using Domain.Repositories;
using Domain.Sales.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ContractRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Contract contract, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Contract>().AddAsync(contract, cancellationToken);

    }

    public async Task<List<Contract>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Contract>()
            .ToListAsync(cancellationToken);
    }

    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Contract>()
            .Where(x => x.Id == id)
            .Include(x => x.Car)
            .Include(x => x.DropDownOffice)
            .Include(x => x.PickUpOffice)
            .Include(x => x.Worker)
            .Include(x => x.Reservation)
            .Include(x => x.ContractItems)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
