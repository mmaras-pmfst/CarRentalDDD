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
    }

    public static class CarCategory
    {
        public static readonly Error CarCategoryAlreadyExists = new Error(
            "CarCategory.CarCategoryAlreadyExists",
            "The specified CarCategory ShortName is already in use");
    }

    public static class CarModel
    {
        public static readonly Error CarModelAlreadyExists = new Error(
            "CarModel.CarModelAlreadyExists",
            "The specified CarModel Name is already in use");
    }

    public static class Office
    {
        public static readonly Error OfficeAlreadyExists = new Error(
            "Office.OfficeAlreadyExists",
            "The specified Office with CityName, StreetName and StreetNumber is already in use");
    }

    
}
