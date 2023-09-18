using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.Offices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetById;

public sealed record OfficeGetByIdQuery(Guid OfficeId) : IQuery<OfficeDetailDto?>;
