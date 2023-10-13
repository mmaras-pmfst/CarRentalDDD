using Domain.Management.CarModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class CarModelDetailDto
{
    public Guid Id { get; set; }
    public CarModelName Name { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal Discount { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public CarCategoryDto Category { get; set; }
    public CarBrandDto CarBrand { get; set; }
    public List<CarDto> Cars { get; set; } = new List<CarDto>();
}
