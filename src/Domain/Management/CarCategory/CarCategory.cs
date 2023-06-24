using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarCategory;
public sealed class CarCategory : AggregateRoot
{
    private readonly List<Guid> _carModelIds = new(); //??
    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public IReadOnlyCollection<Guid> CarModelIds => _carModelIds; //??

    private CarCategory(Guid id, string name, string shortName, string description)
        : base(id)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
    }
    private CarCategory() { }

    public static CarCategory Create(Guid id, string name, string shortName, string description)
    {
        return new CarCategory(id, name, shortName, description);
    }

    public void Update(string name, string shortName, string description)
    {
        Name = name;
        ShortName = shortName;
        Description = description;
    }
}
