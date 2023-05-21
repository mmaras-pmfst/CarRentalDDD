using Domain.CarBrand;
using Domain.CarBrand.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;

internal sealed class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReservationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Reservation>().AddAsync(reservation, cancellationToken);

    }

    public async Task Delete(Guid reservationId, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<Reservation>().Where(x => x.Id == reservationId).ExecuteDeleteAsync(cancellationToken);
    }
}
