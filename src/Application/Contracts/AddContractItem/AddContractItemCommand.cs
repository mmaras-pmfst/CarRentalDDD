using Application.Abstractions;
using Application.Reservations.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Contracts.AddContractItem;
public sealed record AddContractItemCommand(
    Guid ContractId,
    List<ExtrasModel> Extras) : ICommand<Guid>;