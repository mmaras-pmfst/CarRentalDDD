using Domain.Office;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IOfficeRepository
{
    Task AddAsync(Office.Office office, CancellationToken cancellationToken = default);

    Task<List<Office.Office>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Office.Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> AlreadyExists(string city, string streetName, string streetNumber, CancellationToken cancellationToken = default);

    Task Update(Office.Office office, CancellationToken cancellationToken = default);
}
