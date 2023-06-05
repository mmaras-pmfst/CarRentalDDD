using Domain.Repositories;
using Domain.ReservationContract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ReservationContractRepository : IReservationContractRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReservationContractRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(ReservationContract reservationContract, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<ReservationContract>().AddAsync(reservationContract, cancellationToken);

    }

    public async Task<List<ReservationContract>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ReservationContract>()
            .ToListAsync(cancellationToken);
    }

    public async Task<ReservationContract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<ReservationContract>()
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
