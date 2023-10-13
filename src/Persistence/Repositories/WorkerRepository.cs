using Domain.Management.Offices.ValueObjects;
using Domain.Management.Workers;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> AlreadyExists(string personalIdentificationNumber, CancellationToken cancellationToken = default)
    {
        var worker = await _dbContext.Set<Worker>()
                .Where(x => x.PersonalIdentificationNumber == personalIdentificationNumber)
                .SingleOrDefaultAsync(cancellationToken);
        if(worker is null || worker == null)
        {
            return false;
        }
        return true;
    }

    public async Task<List<Worker>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var workers = await _dbContext.Set<Worker>()
                .ToListAsync(cancellationToken);

        return workers;
    }

    public async Task<Worker?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Worker>()
                .Where(x => x.Id == id)
                .Include(x => x.Office)
                .Include(x => x.Contracts)
                .SingleOrDefaultAsync();
    }
}
