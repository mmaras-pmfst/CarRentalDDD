using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class CarDetailDto
{
    public Guid Id { get; set; }
    public string NumberPlate { get; set; }
    public decimal Kilometers { get; set; }
    public byte[]? Image { get; set; }
    public string Status { get; set; }
    public string FuelType { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public CarModelDto CarModel { get; set; }
    public OfficeDto Office { get; set; }
    public List<ContractSummary> Contracts { get; set; }
}
