using Domain.Management.Office.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class WorkerRepository : IWorkerRepository
{
    private readonly ApplicationDbContext _dbContext;
    public WorkerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Worker worker, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(worker, cancellationToken);
    }
}
