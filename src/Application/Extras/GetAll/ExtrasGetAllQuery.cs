using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extras.GetAll;
public sealed record ExtrasGetAllQuery : IQuery<List<ExtraDto>>;