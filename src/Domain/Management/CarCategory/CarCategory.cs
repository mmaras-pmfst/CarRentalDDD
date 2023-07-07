using Domain.Common.Models;
using Domain.Common.ValueObjects;
using Domain.Management.CarCategory.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarCategory;
public sealed class CarCategory : AggregateRoot
{
    private readonly List<Guid> _carModelIds = new(); //??
    public CarCategoryName Name { get; private set; }
    public CarCategoryShortName ShortName { get; private set; }
    public Description Description { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public IReadOnlyCollection<Guid> CarModelIds => _carModelIds; //??

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
