using Domain.Common.Models;
using Domain.Errors;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand.ValueObjects;
public sealed class CarModelName : ValueObject
{
    public const int MaxLength = 15;
    public const int MinLength = 2;
    public string Value { get; private set; }

    private CarModelName()
    {

    }

    private CarModelName(string value)
    {
        Value = value;
    }

    public static Result<CarModelName> Create(string carModelName)
    {
        if (carModelName.Length > MaxLength)
        {
            return Result.Failure<CarModelName>(DomainErrors.CarBrand.CarBrandNameTooLong);

        }
        else if (carModelName.Length < MinLength)
        {
            return Result.Failure<CarModelName>(DomainErrors.CarBrand.CarBrandNameTooShort);

        }

        return new CarModelName(carModelName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;

    }
}
