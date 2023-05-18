using Domain.CarBrand;
using Domain.CarCategory;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

internal sealed class CarBrandRepository : ICarBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CarBrandRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(CarBrand carBrand, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<CarBrand>().AddAsync(carBrand, cancellationToken);
    }

    public async Task<bool> AlreadyExists(string carBrandName, CancellationToken cancellationToken = default)
    {
        var carBrand = await _dbContext.Set<CarBrand>().Where(x => x.CarBrandName.ToUpper() == carBrandName.ToUpper()).SingleOrDefaultAsync(cancellationToken);

        return carBrand != null ? false : true;

    }

    public async Task<List<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarBrand>()
            .Include(x => x.CarModels)
            .ToListAsync(cancellationToken);

    }

    public async Task<CarBrand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarBrand>()
            .Include(x => x.CarModels)
            .Where(x => x.Id == id)
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

    }

    public async Task Update(CarBrand carBrand, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<CarBrand>().Update(carBrand);

    }
}
