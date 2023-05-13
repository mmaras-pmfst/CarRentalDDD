using Domain.CarCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.GetById;

public sealed record CarCategoryGetByIdCommand(Guid id) : IRequest<CarCategory?>;