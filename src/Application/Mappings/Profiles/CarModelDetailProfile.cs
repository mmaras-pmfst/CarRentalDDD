using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.CarModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class CarModelDetailProfile : Profile
{
    public CarModelDetailProfile()
    {
        CreateMap<CarModel, CarModelDetailDto>()
            .ConvertUsing<CarModelDetailConverter>();
    }

    public class CarModelDetailConverter : ITypeConverter<CarModel, CarModelDetailDto>
    {
        public CarModelDetailDto Convert(CarModel source, CarModelDetailDto destination, ResolutionContext context)
        {
            var result = new CarModelDetailDto()
            {
                Id = source.Id,
                Name = source.Name,
                CreatedOnUtc = source.CreatedOnUtc,
                Discount = source.Discount,
                ModifiedOnUtc = source.ModifiedOnUtc,
                PricePerDay = source.PricePerDay,
                CarBrand = new CarBrandDto
                {
                    ModifiedOnUtc = source.CarBrand.ModifiedOnUtc,
                    CreatedOnUtc = source.CarBrand.CreatedOnUtc,
                    Id = source.CarBrand.Id,
                    Name = source.CarBrand.Name
                },
                Category = new CarCategoryDto
                {
                    Id = source.CarCategory.Id,
                    Name = source.CarCategory.Name,
                    CreatedOnUtc = source.CarCategory.CreatedOnUtc,
                    ModifiedOnUtc = source.CarCategory.ModifiedOnUtc,
                    Description = source.CarCategory.Description,
                    ShortName = source.CarCategory.ShortName,
                }
            };
            foreach (var car in source.Cars)
            {
                var oneCar = new CarDto
                {
                    Id = car.Id,
                    CarModelId = car.CarModelId,
                    FuelType = car.FuelType.ToString(),
                    Image = car.Image,
                    Kilometers = car.Kilometers,
                    NumberPlate = car.NumberPlate,
                    OfficeId = car.OfficeId,
                    Status = car.Status.ToString(),
                    CreatedOnUtc= car.CreatedOnUtc,
                    ModifiedOnUtc = car.ModifiedOnUtc,

                };
                result.Cars.Add(oneCar);
            }
            return result;
        }
    }
}
