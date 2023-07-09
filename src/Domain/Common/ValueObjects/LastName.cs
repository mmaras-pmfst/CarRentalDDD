using Domain.Common.Models;
using Domain.Errors;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.ValueObjects;
public sealed class LastName : ValueObject
{
    public const int MaxLength = 20;
    public const int MinLength = 4;

    public string Value { get; private set; }
    private LastName()
    {

    }
    private LastName(string value)
    {
        Value = value;
    }

    public static Result<LastName> Create(string lastName)
    {
        if (string.IsNullOrEmpty(lastName))
        {
            return Result.Failure<LastName>(DomainErrors.LastName.Empty);

        }
        else if (lastName.Length < MinLength)
        {

            return Result.Failure<LastName>(DomainErrors.LastName.TooShort((lastName, MinLength)));
        }
        else if (lastName.Length > MaxLength)
        {
            return Result.Failure<LastName>(DomainErrors.LastName.TooLong((lastName, MinLength)));

        }
        return new LastName(lastName);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
