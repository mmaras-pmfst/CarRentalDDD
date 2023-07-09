using Domain.Common.Models;
using Domain.Errors;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarCategory.ValueObjects;
public sealed class CarCategoryShortName : ValueObject
{
    public const int MaxLength = 10;
    public const int MinLength = 1;
    public string Value { get; private set; }

    private CarCategoryShortName()
    {

    }
    private CarCategoryShortName(string value)
    {
        Value = value;
    }

    public static Result<CarCategoryShortName> Create(string carCategoryShortName)
    {
        if (carCategoryShortName.Length > MaxLength)
        {
            return Result.Failure<CarCategoryShortName>(DomainErrors.CarCategory.CarCategoryShortNameTooLong);

        }
        else if (carCategoryShortName.Length < MinLength)
        {
            return Result.Failure<CarCategoryShortName>(DomainErrors.CarCategory.CarCategoryShortNameTooShort);

        }

        return new CarCategoryShortName(carCategoryShortName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;

    }
}
