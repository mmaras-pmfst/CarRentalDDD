using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICarBrandRepository
{
    Task AddAsync(CarBrand.CarBrand carBrand, CancellationToken cancellationToken = default);
    Task<List<CarBrand.CarBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarBrand.CarBrand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(string carBrandName, CancellationToken cancellationToken = default);
}
