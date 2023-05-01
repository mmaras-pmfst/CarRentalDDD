using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Office;

public sealed class Office : AggregateRoot
{
    private readonly List<Guid> _carIds = new(); // ???
    public string City { get; private set; }
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; }
    public string Country { get; private set; }
    public DateTime OpeningTime { get; private set; }
    public DateTime ClosingTime { get; private set; }
    public string PhoneNumber { get; private set; }
    public IReadOnlyCollection<Guid> CarIds => _carIds; // ???

    private Office(Guid id, string city, string streetName, string country, DateTime openingTime, DateTime closingTime, string phoneNumber)
        : base(id)
    {
        City = city;
        StreetName = streetName;
        Country = country;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        PhoneNumber = phoneNumber;


    }

    private Office()
    {

    }

    public static Office Create(Guid id, string city, string streetName, string country, DateTime openingTime, DateTime closingTime, string phoneNumber)
    {
        return new Office(id, city, streetName, country, openingTime, closingTime, phoneNumber);
    }
}
