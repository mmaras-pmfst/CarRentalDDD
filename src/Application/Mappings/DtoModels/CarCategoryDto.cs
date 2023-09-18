using Domain.Management.CarCategories.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class CarCategoryDto
{
    public Guid Id { get; set; }
    public CarCategoryName Name { get; set; }
    public CarCategoryShortName ShortName { get; set; }
    public Description Description { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}
