using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.Create;

public sealed record CarModelCreateCommand(
    string CarModelName, 
    decimal BasePricePerDay,
    Guid CarBrandId, 
    Guid CarCategoryId) : IRequest<Unit>;
