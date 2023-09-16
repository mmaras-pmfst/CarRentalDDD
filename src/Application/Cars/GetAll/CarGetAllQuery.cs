using Application.Abstractions;
using Domain.Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cars.GetAll;
public sealed record CarGetAllQuery() : IQuery<List<Car>>;