using Application.Abstractions;
using Domain.Sales.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.GetById;
public sealed record GetByIdContractQuery(Guid ContractId) : IQuery<Contract?>;