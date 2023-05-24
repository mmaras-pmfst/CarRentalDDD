using Domain.CarBrand.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

    //public async Task<bool> AlreadyExists(string carModelName, CancellationToken cancellationToken = default)
    //{
    //    var carModel = await _dbContext.Set<CarModel>().Where(x => x.CarModelName.ToUpper() == carModelName.ToUpper()).SingleOrDefaultAsync(cancellationToken);

    //    return carModel != null ? false : true;
    //}

    //public async Task<List<CarModel>> GetAllAsync(CancellationToken cancellationToken = default)
    //{
    //    return await _dbContext.Set<CarModel>().ToListAsync(cancellationToken);
    //}

    //public async Task<CarModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    //{
    //    return await _dbContext.Set<CarModel>().Where(x => x.Id == id).AsNoTracking().SingleOrDefaultAsync(cancellationToken);
    //}

    //public async Task Update(CarModel carModel, CancellationToken cancellationToken = default)
    //{
    //    _dbContext.Set<CarModel>().Update(carModel);
    //}
}
