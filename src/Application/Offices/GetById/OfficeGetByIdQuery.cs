using Application.Abstractions;
using Domain.Management.Office;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetById;

public sealed record OfficeGetByIdQuery(Guid OfficeId) : IQuery<Office?>;
