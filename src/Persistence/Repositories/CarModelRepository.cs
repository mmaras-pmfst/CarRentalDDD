using Domain.Management.CarBrand.Entities;
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

}
