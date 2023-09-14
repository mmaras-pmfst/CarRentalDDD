﻿using Domain.Repositories;
using Domain.Sales.Reservations.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
internal sealed class ReservationDetailRepository : IReservationItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ReservationDetailRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task AddAsync(ReservationItem reservationItem, CancellationToken cancellationToken = default)
    {
        await _dbContext.Set<ReservationItem>().AddAsync(reservationItem, cancellationToken);
    }
}
