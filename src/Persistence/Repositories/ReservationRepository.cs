using Domain.Repositories;
using Domain.Sales.Reservations;
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

    public async Task<List<Reservation>> GetAllAsync(DateTime? dateFrom, DateTime? dateTo, CancellationToken cancellationToken = default)
    {
        var reservations = await _dbContext.Set<Reservation>()
                .Where(x => (dateFrom == null || x.CreatedOnUtc >=dateFrom) && (dateTo == null || x.CreatedOnUtc<=dateTo))
                .ToListAsync(cancellationToken);

        return reservations;
    }

    public async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<Reservation>()
                .Where(x => x.Id == id)
                .Include(x => x.ReservationItems)
                    .ThenInclude(x => x.Extra)
                .Include(x => x.CarModel)
                .Include(x => x.DropDownOffice)
                .Include(x => x.PickUpOffice)
                .SingleOrDefaultAsync();
    }
}
