using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.Update;

public sealed record CarCategoryUpdateCommand(Guid id, string name, string shortName, string description) : IRequest<Unit>;
