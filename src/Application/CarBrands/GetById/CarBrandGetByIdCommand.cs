using Domain.CarBrand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetById;

public sealed record CarBrandGetByIdCommand(Guid id) : IRequest<CarBrand?>;