using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.Update;

public sealed record ColorUpdateCommand(
    Guid ColorId, 
    string ColorName) : ICommand<bool>;
