using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.Create;

public sealed record CarBrandCreateCommand(string CarBrandName): ICommand<Guid>;
