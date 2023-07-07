using Domain.Management.Color;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface IColorRepository
{
    Task AddAsync(Color color, CancellationToken cancellationToken = default);
    Task<List<Color>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Color?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> AlreadyExists(string colorName, CancellationToken cancellationToken = default);
}
