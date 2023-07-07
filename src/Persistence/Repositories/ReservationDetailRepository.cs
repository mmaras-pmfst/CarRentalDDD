using Domain.Repositories;
using Domain.Sales.CarModelRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ReservationDetailRepository : IReservationDetailRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReservationDetailRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(ReservationDetail reservationDetail, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<ReservationDetail>().AddAsync(reservationDetail, cancellationToken);
    }
}
