using Domain.Management.Office;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class OfficeRepository : IOfficeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OfficeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Office office, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<Office>().AddAsync(office);
        }

        public async Task<bool> AlreadyExists(string city, string streetName, string streetNumber, CancellationToken cancellationToken = default)
        {
            var office = await _dbContext.Set<Office>()
                .Where(x => x.City == city && x.StreetName == streetName && x.StreetNumber == streetName)
                .SingleOrDefaultAsync(cancellationToken);

            return office != null ? false : true;

        }

        public async Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var offices = await _dbContext.Set<Office>()
                .Include(x => x.Workers)
                .ToListAsync(cancellationToken);

            return offices;
        }


        public async Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Office>()
                .Include(x => x.Workers)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

    }
}
