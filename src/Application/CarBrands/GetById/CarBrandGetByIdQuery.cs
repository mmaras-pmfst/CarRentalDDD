using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.CarBrands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetById;

public sealed record CarBrandGetByIdQuery(Guid CarBrandId) : IQuery<CarBrandCarModelDto?>;