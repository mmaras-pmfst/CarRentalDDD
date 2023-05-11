using Domain.Office;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetById
{
    public sealed record OfficeGetByIdCommand(Guid id) : IRequest<Office?>;
}
