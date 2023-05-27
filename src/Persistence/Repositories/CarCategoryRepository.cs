using Domain.CarCategory;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

internal sealed class CarCategoryRepository : ICarCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CarCategoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CarCategory carCategory, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<CarCategory>().AddAsync(carCategory,cancellationToken);
    }

    public async Task<bool> AlreadyExists(string shortName, CancellationToken cancellationToken = default)
    {
        var carCategory = await _dbContext.Set<CarCategory>().Where(x => x.ShortName.ToUpper() == shortName.ToUpper()).SingleOrDefaultAsync(cancellationToken);

        return carCategory != null ? false : true;
    }

    public async Task<List<CarCategory>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarCategory>().ToListAsync(cancellationToken);
    }

    public async Task<CarCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarCategory>()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }

}
