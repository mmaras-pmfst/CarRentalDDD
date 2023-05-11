using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Update;

public sealed record ColorUpdateCommand(Guid id, string colorName) : IRequest<Unit>;
