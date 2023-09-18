using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class CarDetailProfile : Profile
{
    public CarDetailProfile()
    {
        CreateMap<Car, CarDetailDto>()
            .ConvertUsing<CarDetailConverter>();
    }

    public class CarDetailConverter : ITypeConverter<Car, CarDetailDto>
    {
        public CarDetailDto Convert(Car source, CarDetailDto destination, ResolutionContext context)
        {
            var result = new CarDetailDto
            {
                Id = source.Id,
                FuelType = source.FuelType.ToString(),
                Image = source.Image,
                NumberPlate = source.NumberPlate,
                Status = source.Status.ToString(),
                Kilometers = source.Kilometers,
                CreatedOnUtc = source.CreatedOnUtc,
                ModifiedOnUtc = source.ModifiedOnUtc,
                CarModel = new CarModelDto
                {
                    ModifiedOnUtc= source.CarModel.ModifiedOnUtc,
                    CreatedOnUtc= source.CarModel.CreatedOnUtc,
                    CarBrandId = source.CarModel.CarBrandId,
                    CarCategoryId= source.CarModel.CarCategoryId,
                    Discount = source.CarModel.Discount,
                    Id= source.CarModel.Id,
                    Name = source.CarModel.Name,
                    PricePerDay = source.CarModel.PricePerDay 
                },
                Office = new OfficeDto
                {
                    Id = source.Office.Id,
                    Address = source.Office.Address,
                    ClosingTime= source.Office.ClosingTime,
                    CreatedOnUtc= source.Office.CreatedOnUtc,
                    ModifiedOnUtc= source.Office.ModifiedOnUtc,
                    OpeningTime= source.Office.OpeningTime,
                    PhoneNumber = source.Office.PhoneNumber
                }
            };
            foreach (var contract in source.Contracts)
            {
                var oneContract = new ContractSummary
                {
                    ContractId = contract.Id,
                    RentalPrice = contract.RentalPrice,
                    TotalPrice = contract.TotalPrice,
                };
                result.Contracts.Add(oneContract);
            }
            return result;
        }
    }
}
