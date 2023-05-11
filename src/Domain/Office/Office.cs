using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Office;

public sealed class Office : AggregateRoot, IAuditableEntity
{
    //private readonly List<Guid> _carIds = new(); // ???
    public string City { get; private set; }
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; }
    public string Country { get; private set; }
    public DateTime? OpeningTime { get; private set; }
    public DateTime? ClosingTime { get; private set; }
    public string PhoneNumber { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    //public IReadOnlyCollection<Guid> CarIds => _carIds; // ???

    private Office(Guid id, string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber)
        :base(id)
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

    public static Office Create(Guid id, string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber)
    {
        return new Office(id, country, city, streetName, streetNumber, openingTime, closingTime, phoneNumber);
    }

    public static Office Update(Guid id, string country, string city, string streetName, string streetNumber, DateTime? openingTime, DateTime? closingTime, string phoneNumber)
    {
        return new Office(id, country, city, streetName, streetNumber, openingTime, closingTime, phoneNumber);
    }
}
