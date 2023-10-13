using Application.Abstractions;
using Application.Mappings.DtoModels;
using Domain.Management.CarCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetById;

public sealed record CarCategoryGetByIdQuery(Guid CarCategoryId) : IQuery<CarCategoryDetailDto?>;