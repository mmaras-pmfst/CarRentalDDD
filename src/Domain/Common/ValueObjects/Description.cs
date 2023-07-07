using Domain.Common.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.ValueObjects;
public sealed class Description : ValueObject
{
    public const int MaxLength = 250;
    public string Value { get; }

    public Description(string value)
    {
        Value = value;
    }

    public static Result<Description> Create(string description)
    {
        if(description == null)
        {
            return Result.Failure<Description>(new Error(
                "DescriptionIsNull",
                $"The Description field cannot be null"));
        }
        else if(description.Length> MaxLength)
        {
            return Result.Failure<Description>(new Error(
                "DescriptionTooLong",
                $"The Description field is longer then {MaxLength}"));
        }
        return new Description(description);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
