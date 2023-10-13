using Domain.Management.Offices;
using Domain.Management.Offices.ValueObjects;
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

        public async Task<bool> AlreadyExists(Address address, CancellationToken cancellationToken = default)
        {
            var office = _dbContext.Set<Office>()
                .AsEnumerable().FirstOrDefault(x => x.Address.Equals(address));

            if(office == null || office is null)
            {
                return false;
            }
            return true;

        }

        public async Task<List<Office>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var offices = await _dbContext.Set<Office>()
                .ToListAsync(cancellationToken);

            return offices;
        }


        public async Task<Office?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Office>()
                .Include(x => x.Workers)
                .Include(x => x.Cars)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

    }
}
