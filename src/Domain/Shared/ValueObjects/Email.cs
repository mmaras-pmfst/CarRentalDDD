using Domain.Common.Models;
using Domain.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; private set; }

    private Email()
    {

    }
    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result.Failure<Email>(DomainErrors.Email.Empty);
        }

        if (email.Split('@').Length != 2)
        {
            return Result.Failure<Email>(DomainErrors.Email.InvalidFormat);
        }

        return new Email(email);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
