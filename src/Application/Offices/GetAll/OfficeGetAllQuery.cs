using Application.Abstractions;
using Domain.Office;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Offices.GetAll;

public sealed record OfficeGetAllQuery() : IQuery<List<Office>>;
