using Domain.ReservationContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IReservationContractRepository
{
    Task AddAsync(ReservationContract.ReservationContract reservationContract, CancellationToken cancellationToken = default);
    Task<List<ReservationContract.ReservationContract>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationContract.ReservationContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
