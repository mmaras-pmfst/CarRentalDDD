using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.Profiles;
public class WorkerDetailProfile : Profile
{
    public WorkerDetailProfile()
    {
        CreateMap<Worker, WorkerDetailDto>()
            .ConvertUsing<WorkerDetailConverter>();
    }

    public class WorkerDetailConverter : ITypeConverter<Worker, WorkerDetailDto>
    {
        public WorkerDetailDto Convert(Worker source, WorkerDetailDto destination, ResolutionContext context)
        {
            var result = new WorkerDetailDto
            {
                CreatedOnUtc = source.CreatedOnUtc,
                Email = source.Email,
                FirstName = source.FirstName,
                Id = source.Id,
                LastName = source.LastName,
                ModifiedOnUtc = source.ModifiedOnUtc,
                PersonalIdentificationNumber = source.PersonalIdentificationNumber,
                PhoneNumber = source.PhoneNumber,
                Office = new OfficeDto
                {
                    PhoneNumber = source.Office.PhoneNumber,
                    ModifiedOnUtc = source.Office.ModifiedOnUtc,
                    Id = source.Office.Id,
                    Address = source.Office.Address,
                    ClosingTime = source.Office.ClosingTime,
                    CreatedOnUtc = source.Office.CreatedOnUtc,
                    OpeningTime = source.Office.OpeningTime,
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
