using Domain.Management.CarModels;
using Domain.Management.CarModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICarModelRepository
{
    Task AddAsync(CarModel carModel, CancellationToken cancellationToken = default);
    Task<List<CarModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CarModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(CarModelName carModelName, Guid carBrandId, Guid carCategoryId, CancellationToken cancellationToken = default);
}
