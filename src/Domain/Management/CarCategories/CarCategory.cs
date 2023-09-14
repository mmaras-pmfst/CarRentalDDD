using Domain.Common.Models;
using Domain.Management.CarCategories.ValueObjects;
using Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarCategories;

public sealed class CarCategory : AggregateRoot
{
    public CarCategoryName Name { get; private set; }
    public CarCategoryShortName ShortName { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private CarCategory(Guid id, CarCategoryName name, CarCategoryShortName shortName, Description description)
        : base(id)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
    }
    private CarCategory() { }

    public static CarCategory Create(Guid id, CarCategoryName name, CarCategoryShortName shortName, Description description)
    {
        return new CarCategory(id, name, shortName, description);
    }

    public void Update(CarCategoryName name, CarCategoryShortName shortName, Description description)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
    }
}
