using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Shared.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; private set; }

    private PhoneNumber()
    {

    }
    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string phoneNumber)
    {
        string Pattern = "^[0-9]+$";
        if (string.IsNullOrEmpty(phoneNumber))
        {
            return Result.Failure<PhoneNumber>(new Error(
                "PhoneNumber.NullValue",
                "PhoneNumber field cannot be empty"));
        }
        else if (!Regex.IsMatch(phoneNumber, Pattern))
        {
            return Result.Failure<PhoneNumber>(new Error(
                "PhoneNumber.InvalidFormat",
                "PhoneNumber field is not in valid format"));
        }

        return new PhoneNumber(phoneNumber);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
