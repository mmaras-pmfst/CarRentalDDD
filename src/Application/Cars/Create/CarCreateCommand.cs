using Application.Abstractions;
using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.Create;
public sealed record CarCreateCommand(
    string NumberPlate,
    string Name,
    decimal Kilometers,
    byte[]? Image,
    CarStatus CarStatus,
    FuelType FuelType,
    Guid ColorId,
    Guid CarModelId,
    Guid OfficeId) :ICommand<Guid>;