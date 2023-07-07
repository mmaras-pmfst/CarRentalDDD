using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Workers.Update;
public sealed record WorkerUpdateCommand(
    Guid WorkerId,
    string Email,
    string PhoneNumber,
    Guid OfficeId) : ICommand<bool>;