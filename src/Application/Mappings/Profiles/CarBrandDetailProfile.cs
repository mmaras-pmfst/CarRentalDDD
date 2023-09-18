using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandProfile;

namespace Application.Mappings.Profiles;
public class CarBrandDetailProfile : Profile
{
    public CarBrandDetailProfile()
    {
        CreateMap<CarBrand, CarBrandCarModelDto>()
            .ConvertUsing<CarBrandDetailConverter>();
    }
    public class CarBrandDetailConverter : ITypeConverter<CarBrand, CarBrandCarModelDto>
    {
        public CarBrandCarModelDto Convert(CarBrand source, CarBrandCarModelDto destination, ResolutionContext context)
        {
            var result = new CarBrandCarModelDto
            {
                CreatedOnUtc = source.CreatedOnUtc,
                Id = source.Id,
                ModifiedOnUtc = source.ModifiedOnUtc,
                Name = source.Name,
            };
            foreach (var carmodel in source.CarModels)
            {
                var oneCarModel = new CarModelDto
                {
                    Id = carmodel.Id,
                    Name = carmodel.Name,
                    CarCategoryId = carmodel.CarCategoryId,
                    CreatedOnUtc = carmodel.CreatedOnUtc,
                    Discount = carmodel.Discount,
                    PricePerDay = carmodel.PricePerDay,
                    ModifiedOnUtc = carmodel.ModifiedOnUtc,
                    CarBrandId = carmodel.CarBrandId,

                };
                result.CarModels.Add(oneCarModel);
            }
            return result;

        }
    }
}
