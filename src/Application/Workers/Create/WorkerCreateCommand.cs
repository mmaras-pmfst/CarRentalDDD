using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Create;
public sealed record WorkerCreateCommand(
    string PersonalIdentificationNumber,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    Guid OfficeId) : ICommand<Guid>;