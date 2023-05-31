using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Create;

public sealed record ColorCreateCommand(string ColorName) : ICommand<Guid>;
