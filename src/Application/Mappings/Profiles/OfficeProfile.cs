using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Offices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<List<Office>, List<OfficeDto>>()
            .ConvertUsing<OfficeConverter>();
    }

    public class OfficeConverter : ITypeConverter<List<Office>, List<OfficeDto>>
    {
        public List<OfficeDto> Convert(List<Office> source, List<OfficeDto> destination, ResolutionContext context)
        {
            var result = new List<OfficeDto>();
            foreach (var item in source)
            {
                var oneOffice = new OfficeDto
                {
                    Id = item.Id,
                    Address = item.Address,
                    ClosingTime = item.ClosingTime,
                    CreatedOnUtc = item.CreatedOnUtc,
                    ModifiedOnUtc = item.ModifiedOnUtc,
                    OpeningTime = item.OpeningTime,
                    PhoneNumber = item.PhoneNumber,
                };
                result.Add(oneOffice);
            }
            return result;
        }
    }
}
