using Domain.Common.Models;
using Domain.Errors;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand.ValueObjects;
public sealed class CarBrandName : ValueObject
{
    public const int MaxLength = 30;
    public const int MinLength = 2;

    public string Value { get; private set; }

    private CarBrandName()
    {

    }
    private CarBrandName(string value)
    {
        Value = value;
    }

    public static Result<CarBrandName> Create(string carBrandName)
    {
        if (carBrandName.Length > MaxLength)
        {
            return Result.Failure<CarBrandName>(DomainErrors.CarBrand.CarBrandNameTooLong);

        }
        else if(carBrandName.Length< MinLength)
        {
            return Result.Failure<CarBrandName>(DomainErrors.CarBrand.CarBrandNameTooShort);

        }
        return new CarBrandName(carBrandName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
