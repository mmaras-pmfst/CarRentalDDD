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
public class CarCategoryProfile : Profile
{
    public CarCategoryProfile()
    {
        CreateMap<List<CarCategory>, List<CarCategoryDto>>()
            .ConvertUsing<CarCategoryConverter>();
    }

    public class CarCategoryConverter : ITypeConverter<List<CarCategory>, List<CarCategoryDto>>
    {
        public List<CarCategoryDto> Convert(List<CarCategory> source, List<CarCategoryDto> destination, ResolutionContext context)
        {
            var result = new List<CarCategoryDto>();
            foreach (var item in source)
            {
                var oneCarCategory = new CarCategoryDto
                {
                    Id = item.Id,
                    CreatedOnUtc = item.CreatedOnUtc,
                    Name = item.Name,
                    Description = item.Description,
                    ModifiedOnUtc = item.ModifiedOnUtc,
                    ShortName = item.ShortName,
                };
                result.Add(oneCarCategory);
            }
            return result;
        }
    }
}
