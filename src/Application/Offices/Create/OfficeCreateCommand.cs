using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.Create;

public sealed record OfficeCreateCommand(
    string Country, 
    string City, 
    string StreetName,
    string StreetNumber, 
    DateTime? OpeningTime, 
    DateTime? ClosingTime, 
    string PhoneNumber) : ICommand<Guid>;
