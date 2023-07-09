using Domain.Management.CarBrand;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class Color
    {
        public static readonly Error ColorAlreadyExists = new Error(
        "Color.ColorAlreadyExists",
        "The specified Color Name is already in use");
    }

    public static class CarBrand
    {
        public static readonly Error CarBrandAlreadyExists = new Error(
            "CarBrand.CarBrandAlreadyExists",
            "The specified CarBrand Name is already in use");

        public static readonly Error CarBrandNameTooLong = new Error(
            "CarBrand.CarBrandNameTooLong",
            $"The specified CarBrand Name is longer than {Management.CarBrand.ValueObjects.CarBrandName.MaxLength}");

        public static readonly Error CarBrandNameTooShort = new Error(
            "CarBrand.CarBrandNameTooShort",
            $"The specified CarBrand Name is shorter than {Management.CarBrand.ValueObjects.CarBrandName.MinLength} ");
    }

    public static class CarCategory
    {
        public static readonly Error CarCategoryAlreadyExists = new Error(
            "CarCategory.CarCategoryAlreadyExists",
            "The specified CarCategory ShortName is already in use");

        public static readonly Error CarCategoryNameTooLong = new Error(
            "CarBrand.CarCategoryNameTooLong",
            $"The specified CarCategory Name is longer than {Management.CarCategory.ValueObjects.CarCategoryName.MaxLength}");

        public static readonly Error CarCategoryNameTooShort = new Error(
            "CarBrand.CarCategoryNameTooShort",
            $"The specified CarCategory Name is shorter than {Management.CarCategory.ValueObjects.CarCategoryName.MinLength} ");

        public static readonly Error CarCategoryShortNameTooLong = new Error(
            "CarBrand.CarCategoryShortNameTooLong",
            $"The specified CarCategory ShortName is longer than {Management.CarCategory.ValueObjects.CarCategoryShortName.MaxLength}");

        public static readonly Error CarCategoryShortNameTooShort = new Error(
            "CarBrand.CarCategoryShortNameTooShort",
            $"The specified CarCategory ShortName is shorter than {Management.CarCategory.ValueObjects.CarCategoryShortName.MinLength} ");
    }

    public static class CarModel
    {
        public static readonly Error CarModelAlreadyExists = new Error(
            "CarModel.CarModelAlreadyExists",
            "The specified CarModel Name is already in use");

        public static readonly Error CarModelNameTooLong = new Error(
            "CarBrand.CarModelNameTooLong",
            $"The specified CarModel Name is longer than {Management.CarBrand.ValueObjects.CarModelName.MaxLength}");

        public static readonly Error CarModelNameTooShort = new Error(
            "CarBrand.CarModelNameTooShort",
            $"The specified CarName Name is shorter than {Management.CarBrand.ValueObjects.CarModelName.MinLength} ");
    }

    public static class Office
    {
        public static readonly Error OfficeAlreadyExists = new Error(
            "Office.OfficeAlreadyExists",
            "The specified Office with CityName, StreetName and StreetNumber is already in use");
    }

    public static class Extra
    {
        public static readonly Error ExtraAlreadyExists = new Error(
            "Extra.ExtraAlreadyExists",
            "The specified Extra Name is already in use");
    }

    public static class Worker
    {
        public static readonly Error WorkerAlreadyExists = new Error(
            "Worker.WorkerAlreadyExists",
            "The Worker with PersonalIdentificationNumber is already in use");
    }

    public static class Email
    {
        public static readonly Error Empty = new(
            "Email.Empty",
            "Email is empty");

        public static readonly Error InvalidFormat = new(
            "Email.InvalidFormat",
            "Email format is invalid");
    }


}
