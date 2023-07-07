using Domain.Sales.CarModelRent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories;
public interface ICarModelRentRepository
{
    Task AddAsync(CarModelRent carModelRent, CancellationToken cancellationToken = default);
}
