using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.CarBrands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.GetAll;

public sealed record CarBrandGetAllQuery() : IQuery<List<CarBrandDto>>;