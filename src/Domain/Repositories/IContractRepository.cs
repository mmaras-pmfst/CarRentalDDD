using Domain.Sales.Contracts;
using Domain.Sales.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IContractRepository
{
    Task AddAsync(Contract contract, CancellationToken cancellationToken = default);
    Task<List<Contract>> GetAllAsync(DateTime? DateFrom,DateTime? DateTo, CancellationToken cancellationToken = default);
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
