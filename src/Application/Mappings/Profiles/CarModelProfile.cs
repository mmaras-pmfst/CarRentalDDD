using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandProfile;

namespace Application.Mappings.Profiles;
public class CarModelProfile : Profile
{
    public CarModelProfile()
    {
        CreateMap<List<CarModel>, List<CarModelDto>>()
            .ConvertUsing<CarModelConverter>();
    }
    public class CarModelConverter : ITypeConverter<List<CarModel>, List<CarModelDto>>
    {
        public List<CarModelDto> Convert(List<CarModel> source, List<CarModelDto> destination, ResolutionContext context)
        {
            var result = new List<CarModelDto>();
            foreach (var item in source)
            {
                var oneCarModel = new CarModelDto
                {
                    Id = item.Id,
                    CarBrandId = item.CarBrandId,
                    CarCategoryId = item.CarCategoryId,
                    CreatedOnUtc = item.CreatedOnUtc,
                    Discount = item.Discount,
                    ModifiedOnUtc = item.ModifiedOnUtc,
                    Name = item.Name,
                    PricePerDay = item.PricePerDay,

                };
                result.Add(oneCarModel);
            }
            return result;
        }
    }
}
