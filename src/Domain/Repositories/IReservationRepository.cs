using Domain.CarBrand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
    //Task<List<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);
    //Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    //Task<bool> AlreadyExists(string carBrandName, CancellationToken cancellationToken = default);
    Task Delete(Guid reservationId, CancellationToken cancellationToken = default);
}
