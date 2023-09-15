using Application.Common.Mailing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Events;
public record ContractCreatedEvent : INotification
{
    public string Email { get; init; }
    public decimal RentalPrice { get; init; }
    public decimal TotalPrice { get; init; }
    public string CarModelName { get; init; }
    public string CarBrandName { get; init; }
    public string RegistrationPlate { get; init; }
    public EmailType Type { get; init; }
}
