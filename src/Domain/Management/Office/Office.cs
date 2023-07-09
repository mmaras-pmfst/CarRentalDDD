using Domain.Common.Models;
using Domain.Common.ValueObjects;
using Domain.Management.CarBrand.Entities;
using Domain.Management.Office.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Office;
public sealed class Office : AggregateRoot, IAuditableEntity
{
    public readonly List<Worker> _workers = new();
    private readonly List<Guid> _carIds = new(); // ???
    public string City { get; private set; }
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; }
    public string Country { get; private set; }
    public DateTime? OpeningTime { get; private set; }
    public DateTime? ClosingTime { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public IReadOnlyCollection<Guid> CarIds => _carIds; // ???
    public IReadOnlyCollection<Worker> Workers => _workers;


    private Office(Guid id, string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
        : base(id)
    {
        Country = country;
        City = city;
        StreetName = streetName;
        StreetNumber = streetNumber;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;
    }


    private Office()
    {

    }

    public static Office Create(Guid id, string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
    {
        return new Office(id, country, city, streetName, streetNumber, openingTime, closingTime, phoneNumber);
    }

    public void Update(string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, PhoneNumber phoneNumber)
    {
        Country = country;
        City = city;
        StreetName = streetName;
        StreetNumber = streetNumber;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;
    }

    public Worker AddWorker(string personalIdentificationNumber, FirstName firstName, string lastName, Email email, PhoneNumber phoneNumber)
    {
        var newWorker = Worker.Create(Guid.NewGuid(), firstName, lastName, email, phoneNumber, this, personalIdentificationNumber);

        _workers.Add(newWorker);

        return newWorker;
    }

    public void UpdateWorker(Guid workerId, Email email, PhoneNumber phoneNumber)
    {
        var worker = _workers.Where(x => x.Id == workerId).SingleOrDefault();
        
        worker!.Update(email, phoneNumber, this);
    }
}
