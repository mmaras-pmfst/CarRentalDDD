using Domain.Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface ICarRepository
{
    Task AddAsync(Car car, CancellationToken cancellationToken = default);
    Task<bool> PlateNumberAlreadyExists(string numberPlate, CancellationToken cancellationToken = default);
    Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Car?> GetByIdAsync(Guid carId, CancellationToken cancellationToken = default);

}
