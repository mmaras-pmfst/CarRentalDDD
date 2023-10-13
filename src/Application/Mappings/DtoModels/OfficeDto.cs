using Domain.Management.Offices.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class OfficeDto
{
    public Guid Id { get; set; }
    public Address Address { get; set; }
    public DateTime? OpeningTime { get; set; }
    public DateTime? ClosingTime { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
