using Domain.Common.Models;
using Domain.Management.Cars;
using Domain.Management.Offices.ValueObjects;
using Domain.Management.Workers;
using Domain.Sales.Contracts;
using Domain.Sales.Reservations;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Offices;

public sealed class Office : AggregateRoot, IAuditableEntity
{
    public Address Address { get; private set; }
    public DateTime? OpeningTime { get; private set; }
    public DateTime? ClosingTime { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public IReadOnlyCollection<Worker> Workers { get; private set; }
    public IReadOnlyCollection<Car> Cars { get; private set; }
    //public IReadOnlyCollection<Contract> Contracts { get; private set; }
    //public IReadOnlyCollection<Reservation> Reservations { get; private set; }

    private Office(Guid id, Address address, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
        : base(id)
    {
        Address= address;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;
    }


    private Office()
    {

    }

    public static Office Create(Guid id, Address address, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
    {
        return new Office(id, address, openingTime, closingTime, phoneNumber);
    }

    public void Update(Address address, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
    {
        Address = address;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;
    }
}
