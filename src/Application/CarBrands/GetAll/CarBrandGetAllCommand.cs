using Domain.CarBrand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetAll;

public sealed record CarBrandGetAllCommand() : IRequest<List<CarBrand>>;