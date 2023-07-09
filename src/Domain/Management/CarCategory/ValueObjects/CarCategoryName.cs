using Domain.Common.Models;
using Domain.Errors;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarCategory.ValueObjects;
public sealed class CarCategoryName : ValueObject
{
    public const int MaxLength = 30;
    public const int MinLength = 3;
    public string Value { get; private set; }

    private CarCategoryName()
    {

    }
    private CarCategoryName(string value)
    {
        Value = value;
    }

    public static Result<CarCategoryName> Create(string carCategoryName)
    {
        if (carCategoryName.Length > MaxLength)
        {
            return Result.Failure<CarCategoryName>(DomainErrors.CarCategory.CarCategoryNameTooLong);

        }
        else if (carCategoryName.Length < MinLength)
        {
            return Result.Failure<CarCategoryName>(DomainErrors.CarCategory.CarCategoryNameTooShort);

        }

        return new CarCategoryName(carCategoryName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;

    }
}
