using Domain.Management.CarBrands;
using Domain.Management.CarBrands.ValueObjects;
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

    public async Task<bool> AlreadyExists(CarBrandName carBrandName, CancellationToken cancellationToken = default)
    {
        var carBrand = await _dbContext.Set<CarBrand>()
            .Where(x => x.Name == carBrandName)
            .SingleOrDefaultAsync(cancellationToken);

        return carBrand != null ? false : true;

    }

    public async Task<List<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarBrand>()
            .ToListAsync(cancellationToken);

    }

    public async Task<CarBrand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var carBrand = await _dbContext.Set<CarBrand>()
            .Include(x => x.CarModels)
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
        return carBrand;

    }

}
