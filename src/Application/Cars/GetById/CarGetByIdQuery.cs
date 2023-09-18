using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.GetById;
public sealed record CarGetByIdQuery(Guid CarId) : IQuery<CarDetailDto?>;