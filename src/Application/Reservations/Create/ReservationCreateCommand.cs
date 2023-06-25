﻿using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Reservations.Create;
public sealed record ReservationCreateCommand() : ICommand<Guid>;