using Domain.Color;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetById;

public sealed record ColorGetByIdCommand(Guid id) : IRequest<Color?>;

