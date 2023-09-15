using Domain.Management.Cars;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _dbContext;
    public CarRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Car car, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Car>().AddAsync(car,cancellationToken);
    }

    public async Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var cars = await _dbContext.Set<Car>()
            .ToListAsync(cancellationToken);

        return cars;
    }

    public async Task<Car?> GetByIdAsync(Guid carId, CancellationToken cancellationToken = default)
    {
        var car = await _dbContext.Set<Car>()
            .Where(x => x.Id == carId)
            .Include(x => x.Office)
            .Include(x => x.CarModel)
                .ThenInclude(x => x.CarCategory)
            .Include(x => x.CarModel)
                .ThenInclude(x => x.CarBrand)
            .SingleOrDefaultAsync(cancellationToken);

        return car;
    }

    public async Task<bool> PlateNumberAlreadyExists(string numberPlate, CancellationToken cancellationToken = default)
    {
        var car = await _dbContext.Set<Car>()
            .Where(x => x.NumberPlate == numberPlate)
            .SingleOrDefaultAsync(cancellationToken);

        if(car is null || car == null)
        {
            return false;
        }
        return true;
    }
}
