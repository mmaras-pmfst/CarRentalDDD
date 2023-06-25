using Domain.Repositories;
using Domain.Sales.CarModelRent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class CarModelRentRepository : ICarModelRentRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CarModelRentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(CarModelRent carModelRent, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<CarModelRent>().AddAsync(carModelRent, cancellationToken);
    }
}
