using Domain.Repositories;
using Domain.Sales.Extras;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ExtrasRepository : IExtrasRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ExtrasRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Extra extras, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Extra>().AddAsync(extras);
    }

    public async Task<bool> AlreadyExists(string name, CancellationToken cancellationToken = default)
    {
        var extras = await _dbContext.Set<Extra>()
            .Where(x => x.Name.ToUpper() == name.ToUpper())
            .SingleOrDefaultAsync(cancellationToken);

        if(extras is null || extras == null)
        {
            return false;
        }
        return true;
    }

    public async Task<List<Extra>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var extras = await _dbContext.Set<Extra>()
            .ToListAsync(cancellationToken);

        return extras;
    }

    public async Task<Extra?> GetByIdAsync(Guid extrasId, CancellationToken cancellationToken = default)
    {
        var extra = await _dbContext.Set<Extra>()
            .Where(x => x.Id == extrasId)
            .SingleOrDefaultAsync(cancellationToken);

        return extra;
    }
}
