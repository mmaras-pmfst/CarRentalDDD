using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class ExtraProfile : Profile
{
    public ExtraProfile()
    {
        CreateMap<List<Extra>, List<ExtraDto>>()
            .ConvertUsing<ExtraConverter>();
    }

    public class ExtraConverter : ITypeConverter<List<Extra>, List<ExtraDto>>
    {
        public List<ExtraDto> Convert(List<Extra> source, List<ExtraDto> destination, ResolutionContext context)
        {
            var result = new List<ExtraDto>();
            foreach (var item in source)
            {
                var oneExtra = new ExtraDto
                {
                    Id = item.Id,
                    CreatedOnUtc = item.CreatedOnUtc,
                    Description = item.Description,
                    ModifiedOnUtc = item.ModifiedOnUtc,
                    Name = item.Name,
                    PricePerDay = item.PricePerDay,
                };
                result.Add(oneExtra);
            }
            return result;
        }
    }
}
