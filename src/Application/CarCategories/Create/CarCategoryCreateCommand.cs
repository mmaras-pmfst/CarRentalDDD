using Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CarCategories.Create;

public sealed record CarCategoryCreateCommand(
    string Name, 
    string ShortName, 
    string Description) : ICommand<Guid>;
