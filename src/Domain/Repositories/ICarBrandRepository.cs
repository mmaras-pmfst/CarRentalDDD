using Domain.Management.CarBrands;
using Domain.Management.CarBrands.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICarBrandRepository
{
    Task AddAsync(CarBrand carBrand, CancellationToken cancellationToken = default);
    Task<List<CarBrand>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarBrand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(CarBrandName carBrandName, CancellationToken cancellationToken = default);
}
