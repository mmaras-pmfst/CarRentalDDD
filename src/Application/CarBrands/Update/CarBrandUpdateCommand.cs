using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarBrands.Update;

public sealed record CarBrandUpdateCommand(
    Guid CarBrandId, 
    string CarBrandName) : IQuery<bool>;
