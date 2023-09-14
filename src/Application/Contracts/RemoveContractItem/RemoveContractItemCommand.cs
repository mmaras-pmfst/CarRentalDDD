using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.RemoveContractItem;
public sealed record RemoveContractItemCommand(
    Guid ContractId,
    List<Guid> ExtraIds) : ICommand<Guid>;