using Domain.Management.CarModels;
using Domain.Management.CarModels.ValueObjects;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

internal sealed class CarModelRepository : ICarModelRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CarModelRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(CarModel carModel, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<CarModel>().AddAsync(carModel, cancellationToken);

    }

    public async Task<bool> AlreadyExists(CarModelName carModelName, Guid carBrandId, Guid carCategoryId, CancellationToken cancellationToken = default)
    {
        var carModel = await _dbContext.Set<CarModel>()
            .Where(x => 
                x.Name.Value.ToUpper() == carModelName.Value.ToUpper()
                && x.CarCategoryId == carCategoryId
                && x.CarBrandId == carBrandId)
            .SingleOrDefaultAsync(cancellationToken);

        if(carModel == null || carModel is null)
        {
            return false;
        }
        return true;

    }

    public async Task<List<CarModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarModel>().ToListAsync(cancellationToken);

    }

    public async Task<CarModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<CarModel>()
            .Where(x => x.Id == id)
            .Include(x => x.CarBrand)
            .Include(x => x.CarCategory)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
