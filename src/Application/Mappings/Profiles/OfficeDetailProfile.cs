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
public class OfficeDetailProfile : Profile
{
    public OfficeDetailProfile()
    {
        CreateMap<Office, OfficeDetailDto>()
            .ConvertUsing<OfficeDetailConverter>();
    }

    public class OfficeDetailConverter : ITypeConverter<Office, OfficeDetailDto>
    {
        public OfficeDetailDto Convert(Office source, OfficeDetailDto destination, ResolutionContext context)
        {
            var result = new OfficeDetailDto
            {
                Address = source.Address,
                ClosingTime = source.ClosingTime,
                CreatedOnUtc = source.CreatedOnUtc,
                Id = source.Id,
                ModifiedOnUtc = source.ModifiedOnUtc,
                OpeningTime = source.OpeningTime,
                PhoneNumber = source.PhoneNumber,

            };
            foreach (var car in source.Cars)
            {
                var oneCar = new CarDto
                {
                    ModifiedOnUtc = car.ModifiedOnUtc,
                    Id = car.Id,
                    CarModelId = car.CarModelId,
                    CreatedOnUtc = car.CreatedOnUtc,
                    FuelType = car.FuelType.ToString(),
                    Image = car.Image,
                    Kilometers = car.Kilometers,
                    NumberPlate = car.NumberPlate,
                    OfficeId = car.OfficeId,
                    Status = car.Status.ToString()
                };
                result.Cars.Add(oneCar);
            }
            foreach (var worker in source.Workers)
            {
                var oneWorker = new WorkerDto
                {
                    OfficeId = worker.OfficeId,
                    CreatedOnUtc = worker.CreatedOnUtc,
                    Email = worker.Email,
                    FirstName = worker.FirstName,
                    Id = worker.Id,
                    LastName = worker.LastName,
                    ModifiedOnUtc = worker.ModifiedOnUtc,
                    PersonalIdentificationNumber = worker.PersonalIdentificationNumber,
                    PhoneNumber = worker.PhoneNumber,
                };
                result.Workers.Add(oneWorker);
            }
            return result;
        }
    }
}
