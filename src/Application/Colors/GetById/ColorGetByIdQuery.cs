using Application.Abstractions;
using Domain.Management.Color;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetById;

public sealed record ColorGetByIdQuery(Guid ColorId) : IQuery<Color?>;

