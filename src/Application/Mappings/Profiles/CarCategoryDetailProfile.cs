using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class CarCategoryDetailProfile : Profile
{
    public CarCategoryDetailProfile()
    {
        CreateMap<CarCategory, CarCategoryDetailDto>()
            .ConvertUsing<CarCategoryDetaiConverter>();
    }

    public class CarCategoryDetaiConverter : ITypeConverter<CarCategory, CarCategoryDetailDto>
    {
        public CarCategoryDetailDto Convert(CarCategory source, CarCategoryDetailDto destination, ResolutionContext context)
        {
            var result = new CarCategoryDetailDto
            {
                Id = source.Id,
                CreatedOnUtc = source.CreatedOnUtc,
                Description = source.Description,
                ModifiedOnUtc = source.ModifiedOnUtc,
                Name = source.Name,
                ShortName = source.ShortName,
            };
            foreach (var item in source.CarModels)
            {
                var oneCarModel = new CarModelDto
                {
                    Name = item.Name,
                    Id = item.Id,
                    ModifiedOnUtc = item.ModifiedOnUtc,
                    CarBrandId = item.CarBrandId,
                    CarCategoryId = item.CarCategoryId,
                    CreatedOnUtc = item.CreatedOnUtc,
                    Discount = item.Discount,
                    PricePerDay = item.PricePerDay,
                };
                result.CarModels.Add(oneCarModel);
            }
            return result;
        }
    }
}
