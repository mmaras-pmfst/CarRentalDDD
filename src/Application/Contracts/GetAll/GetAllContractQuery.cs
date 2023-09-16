using Application.Abstractions;
using Domain.Sales.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.GetAll;
public sealed record GetAllContractQuery(
    DateTime? DateFrom, 
    DateTime? DateTo) : IQuery<List<Contract>>;