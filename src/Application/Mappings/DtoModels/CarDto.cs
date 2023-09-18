using Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class CarDto
{
    public Guid Id { get; set; }
    public string NumberPlate { get; set; }
    public decimal Kilometers { get; set; }
    public byte[]? Image { get; set; }
    public string Status { get; set; }
    public string FuelType { get; set; }
    public Guid CarModelId { get; set; }
    public Guid OfficeId { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
