using Application.Abstractions;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.GetById;
public sealed record ExtrasGetByIdQuery(Guid ExtraId) : IQuery<Extra?>;