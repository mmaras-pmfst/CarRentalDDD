using Application.Abstractions;
using Domain.Management.Offices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetAll;

public sealed record OfficeGetAllQuery() : IQuery<List<Office>>;
