using Domain.Management.CarBrands.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class CarBrandDto
{
    public Guid Id { get; set; }
    public CarBrandName Name { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
