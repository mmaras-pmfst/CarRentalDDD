using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.Create;

public sealed record OfficeCreateCommand(string country, string city, string streetName,
    string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber) : IRequest<Unit>;
