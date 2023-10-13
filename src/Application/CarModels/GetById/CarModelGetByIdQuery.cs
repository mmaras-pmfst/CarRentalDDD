using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.CarModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarModels.GetById;

public sealed record CarModelGetByIdQuery(
    Guid CarModelId) : IQuery<CarModelDetailDto?>;