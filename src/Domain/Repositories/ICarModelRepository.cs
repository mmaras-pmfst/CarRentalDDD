using Domain.CarBrand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface ICarModelRepository
{
    Task AddAsync(CarModel carModel, CancellationToken cancellationToken = default);
    //Task<List<CarModel>> GetAllAsync(CancellationToken cancellationToken = default);
    //Task<CarModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<bool> AlreadyExists(string carModelName, CancellationToken cancellationToken = default);
    //Task Update(CarModel carModel, CancellationToken cancellationToken = default);
}
