using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Events;
public record ContractEmailModel(
    decimal RentalPrice,
    decimal TotalPrice,
    string CarModelName,
    string CarBrandName,
    string RegistrationPlate);