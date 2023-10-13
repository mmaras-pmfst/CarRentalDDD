using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.Profiles;
public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<List<Car>, List<CarDto>>()
            .ConvertUsing<CarConverter>();
    }

    public class CarConverter : ITypeConverter<List<Car>, List<CarDto>>
    {
        public List<CarDto> Convert(List<Car> source, List<CarDto> destination, ResolutionContext context)
        {
            var result = new List<CarDto>();
            foreach (var item in source)
            {
                var oneCar = new CarDto
                {
                    Id = item.Id,
                    CarModelId = item.CarModelId,
                    FuelType = item.FuelType.ToString(),
                    Image = item.Image,
                    Kilometers = item.Kilometers,
                    NumberPlate = item.NumberPlate,
                    OfficeId = item.OfficeId,
                    Status = item.Status.ToString(),
                    ModifiedOnUtc= item.ModifiedOnUtc,
                    CreatedOnUtc = item.CreatedOnUtc,
                };
                result.Add(oneCar);
            }
            return result;
        }
    }
}
