using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand.Entities;

public sealed class CarModel : Entity
{
    private readonly List<Guid> _carIds = new();
    public string CarModelName { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }

    public IReadOnlyCollection<Guid> CarIds => _carIds;

    internal CarModel(Guid id, string carModelName, CarBrand carBrand, CarCategory.CarCategory carCategory)
    {
        CarModelName = carModelName;
        CarBrandId = carBrand.Id;
        CarCategoryId = carCategory.Id;
    }
    private CarModel() { }
}
