using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarBrands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.Profiles;
public class CarBrandProfile : Profile
{
    public CarBrandProfile()
    {
        CreateMap<List<CarBrand>, List<CarBrandDto>>()
            .ConvertUsing<CarBrandConverter>();
    }

    public class CarBrandConverter : ITypeConverter<List<CarBrand>, List<CarBrandDto>>
    {
        public List<CarBrandDto> Convert(List<CarBrand> source, List<CarBrandDto> destination, ResolutionContext context)
        {
            var result = new List<CarBrandDto>();
            foreach (var item in source)
            {
                var oneCarBrand = new CarBrandDto
                {
                    Id = item.Id,
                    CreatedOnUtc = DateTime.UtcNow,
                    ModifiedOnUtc = DateTime.UtcNow,
                    Name = item.Name,
                };
                result.Add(oneCarBrand);
            }
            return result;
        }
    }
}
