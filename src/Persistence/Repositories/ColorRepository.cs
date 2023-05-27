using Domain.Color;
using Domain.Office;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal sealed class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ColorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Color color, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(color, cancellationToken);
        }

        public async Task<bool> AlreadyExists(string colorName, CancellationToken cancellationToken = default)
        {
            var color = await _dbContext.Set<Color>()
                .Where(x => x.ColorName == colorName)
                .SingleOrDefaultAsync(cancellationToken);

            return color != null ? false : true;
        }

        public async Task<List<Color>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Color>()
                .ToListAsync(cancellationToken);

        }

        public async Task<Color?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Color>()
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
        }

    }
}
