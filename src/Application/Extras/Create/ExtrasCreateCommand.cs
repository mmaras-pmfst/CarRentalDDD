using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.Create;
public sealed record ExtrasCreateCommand(
    string Name,
    string Description,
    decimal PricePerDay) : ICommand<Guid>;