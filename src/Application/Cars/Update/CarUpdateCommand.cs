using Application.Abstractions;
using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Update;
public sealed record CarUpdateCommand(
    Guid CarId,
    decimal Kilometers,
    byte[]? Image,
    CarStatus CarStatus,
    Guid OfficeId) : ICommand<bool>;