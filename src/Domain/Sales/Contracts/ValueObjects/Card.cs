using Domain.Common.Models;
using Domain.Management.Offices.ValueObjects;
using Domain.Shared;
using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Contracts.ValueObjects;

public sealed class Card : ValueObject
{
    public const int CvvLength = 3;

    public const int CardYearExpirationLength = 2;

    public string? CardName { get; private set; }
    public string? CardNumber { get; private set; }
    public string? CVV { get; private set; }
    public string? CardDateExpiration { get; private set; }
    public string? CardYearExpiration { get; private set; }

    private Card(string? cardName, string? cardNumber, string? cVV, string? cardDateExpiration, string? cardYearExpiration)
    {
        CardName = cardName;
        CardNumber = cardNumber;
        CVV = cVV;
        CardDateExpiration = cardDateExpiration;
        CardYearExpiration = cardYearExpiration;
    }
    private Card()
    {

    }
    public static Result<Card> Create(string? cardName, string? cardNumber, 
        string? CVV, string? cardDateExpiration, string? cardYearExpiration, PaymentMethod paymentMethod)
    {
        if (paymentMethod == PaymentMethod.Card)
        {
            if (string.IsNullOrEmpty(cardName))
            {
                return Result.Failure<Card>(new Error(
                    "CardNameIsNull",
                    $"The CardName field cannot be null"));
            }
            else if (string.IsNullOrEmpty(cardNumber))
            {
                return Result.Failure<Card>(new Error(
                    "CardNumberIsNull",
                    $"The CardNumber field cannot be null"));
            }
            else if (string.IsNullOrEmpty(CVV) || CVV.Length != CvvLength)
            {
                return Result.Failure<Card>(new Error(
                    "CVVIncorectFormat",
                    $"The CVV field cannot be null or longer/shorter then {CvvLength}"));
            }
            else if (string.IsNullOrEmpty(cardName))
            {
                return Result.Failure<Card>(new Error(
                    "CardNameIsNull",
                    $"The CardName field cannot be null"));
            }
            else if (string.IsNullOrEmpty(cardDateExpiration))
            {
                return Result.Failure<Card>(new Error(
                    "CardDateExpirationIsNull",
                    $"The CardDateExpiration field cannot be null"));
            }
            else if (string.IsNullOrEmpty(cardYearExpiration) || cardYearExpiration.Length != CardYearExpirationLength)
            {
                return Result.Failure<Card>(new Error(
                    "CardYearExpirationFormat",
                    $"The CardYearExpiration field cannot be null or longer/shorter then {CardYearExpirationLength}"));
            }
        }

        return new Card(cardName, cardNumber, CVV, cardDateExpiration, cardYearExpiration);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return CardName;
        yield return CardNumber;
        yield return CVV;
        yield return CardDateExpiration;
        yield return CardYearExpiration;
    }

}
