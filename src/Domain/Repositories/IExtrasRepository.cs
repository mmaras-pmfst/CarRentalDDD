using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IExtrasRepository
{
    Task AddAsync(Extra extras, CancellationToken cancellationToken = default);

    Task<Extra?> GetByIdAsync(Guid extrasId, CancellationToken cancellationToken = default);

    Task<List<Extra>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> AlreadyExists(string name, CancellationToken cancellationToken = default);
}
