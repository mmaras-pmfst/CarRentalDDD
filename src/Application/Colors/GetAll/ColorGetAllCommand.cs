using Domain.Color;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Colors.GetAll
{
    public sealed record ColorGetAllCommand() : IRequest<List<Color>>;
}
