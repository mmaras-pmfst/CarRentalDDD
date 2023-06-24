using Domain.Common.Models;
using Domain.Management.CarCategory;
using Domain.Sales.CarModelRent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand.Entities;

public sealed class CarModel : Entity
{

    private readonly List<Guid> _carIds = new();
    private readonly List<CarModelRent> _carModelRents= new();
    public string Name { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }
    public IReadOnlyCollection<Guid> CarIds => _carIds;
    public IReadOnlyCollection<CarModelRent> CarModelRents => _carModelRents;


    internal CarModel(Guid id, string name, Guid carBrandId, Guid carCategoryId)
        : base(id)
    {
        Name = name;
        CarBrandId = carBrandId;
        CarCategoryId = carCategoryId;
    }
    private CarModel() { }

    public void Update(string name, CarCategory.CarCategory carCategory)
    {
        Name = name;
        CarCategoryId = carCategory.Id;
    }

    public static CarModel Create(Guid id, string name, CarBrand carBrand, CarCategory.CarCategory carCategory)
    {
        return new CarModel(id, name, carBrand.Id, carCategory.Id);
    }
}
