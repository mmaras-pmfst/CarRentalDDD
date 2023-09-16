using Domain.Common.Models;
using Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 40;
    public const int MinLength = 2;

    public string Value { get; private set; }
    private FirstName()
    {

    }

    private FirstName(string value)
    {
        Value = value;
    }

    public static Result<FirstName> Create(string firstName)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            return Result.Failure<FirstName>(DomainErrors.FirstName.Empty);

        }
        else if (firstName.Length < MinLength)
        {
            return Result.Failure<FirstName>(DomainErrors.FirstName.TooShort((firstName, MinLength)));

        }
        else if (firstName.Length > MaxLength)
        {
            return Result.Failure<FirstName>(DomainErrors.FirstName.TooLong((firstName, MaxLength)));

        }
        return new FirstName(firstName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
