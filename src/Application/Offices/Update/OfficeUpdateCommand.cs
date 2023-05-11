using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.Update
{
    public sealed record OfficeUpdateCommand(Guid id, string country, string city, string streetName,
    string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber) : IRequest<Unit>;
}
