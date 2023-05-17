using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.Update;

public sealed record CarBrandUpdateCommand(Guid id, string carBrandName) : IRequest<Unit>;
