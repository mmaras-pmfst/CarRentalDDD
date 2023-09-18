using Application.Mappings.DtoModels;
using AutoMapper;
using Domain.Management.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Mappings.Profiles.CarBrandDetailProfile;

namespace Application.Mappings.Profiles;
public class WorkerProfile : Profile
{
    public WorkerProfile()
    {
        CreateMap<List<Worker>, List<WorkerDto>>()
            .ConvertUsing<WorkerConverter>();
    }

    public class WorkerConverter : ITypeConverter<List<Worker>, List<WorkerDto>>
    {
        public List<WorkerDto> Convert(List<Worker> source, List<WorkerDto> destination, ResolutionContext context)
        {
            var result = new List<WorkerDto>();
            foreach (var worker in source)
            {
                var workerDto = new WorkerDto
                {
                    CreatedOnUtc = worker.CreatedOnUtc,
                    Email = worker.Email,
                    FirstName = worker.FirstName,
                    Id = worker.Id,
                    LastName = worker.LastName,
                    ModifiedOnUtc = worker.ModifiedOnUtc,
                    OfficeId = worker.OfficeId,
                    PersonalIdentificationNumber = worker.PersonalIdentificationNumber,
                    PhoneNumber = worker.PhoneNumber,
                };
                result.Add(workerDto);
            }
            return result;
        }
    }
}
