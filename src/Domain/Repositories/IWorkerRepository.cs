using Domain.Management.Offices.ValueObjects;
using Domain.Management.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IWorkerRepository
{
    Task AddAsync(Worker worker, CancellationToken cancellationToken = default);
    Task<List<Worker>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Worker?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(string personalIdentificationNumber, CancellationToken cancellationToken = default);
}
