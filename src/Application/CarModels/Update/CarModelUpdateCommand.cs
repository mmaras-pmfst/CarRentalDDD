using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Update;

public sealed record CarModelUpdateCommand(
    Guid CarModelId,
    decimal BasePricePerDay,
    string CarModelName, 
    Guid CarBrandId, 
    Guid CarCategoryId) : ICommand<bool>;