using Domain.Common.Models;
using Domain.Shared;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Offices.ValueObjects;

public class Address : ValueObject
{
    public string City { get; private set; }
    public string StreetName { get; private set; }
    public string StreetNumber { get; private set; }
    public string Country { get; private set; }

    private Address(string city, string streetName, string streetNumber, string country)
    {
        City = city;
        StreetName = streetName;
        StreetNumber = streetNumber;
        Country = country;
    }

    private Address()
    {

    }

    public static Result<Address> Create(string city, string streetName, string streetNumber, string country)
    {
        if (string.IsNullOrEmpty(city))
        {
            return Result.Failure<Address>(new Error(
                "CityIsNull",
                $"The City field cannot be null"));
        }
        else if (string.IsNullOrEmpty(streetName))
        {
            return Result.Failure<Address>(new Error(
                "StreetNameIsNull",
                $"The StreetName field cannot be null"));
        }
        else if (string.IsNullOrEmpty(streetNumber))
        {
            return Result.Failure<Address>(new Error(
                "StreetNumberIsNull",
                $"The StreetNumber field cannot be null"));
        }
        else if (string.IsNullOrEmpty(country))
        {
            return Result.Failure<Address>(new Error(
                "CountryIsNull",
                $"The Country field cannot be null"));
        }
        return new Address(city, streetName, streetNumber, country);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return City;
        yield return StreetName;
        yield return StreetNumber;
        yield return Country;
    }
}
