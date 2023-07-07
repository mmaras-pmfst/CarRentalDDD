using Domain.Common.Models;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Management.CarCategory;
using Domain.Sales.CarModelRent;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand.Entities;

public sealed class CarModel : Entity
{

    private readonly List<Guid> _carIds = new();
    private readonly List<CarModelRent> _carModelRents = new();
    public CarModelName Name { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }
    public IReadOnlyCollection<Guid> CarIds => _carIds;
    public IReadOnlyCollection<CarModelRent> CarModelRents => _carModelRents;


    internal CarModel(Guid id, CarModelName name, Guid carBrandId, Guid carCategoryId)
        : base(id)
    {
        Name = name;
        CarBrandId = carBrandId;
        CarCategoryId = carCategoryId;
    }
    private CarModel() { }

    public void Update(CarModelName name, CarCategory.CarCategory carCategory)
    {
        Name = name;
        CarCategoryId = carCategory.Id;
    }

    public static CarModel Create(Guid id, CarModelName name, CarBrand carBrand, CarCategory.CarCategory carCategory)
    {
        return new CarModel(id, name, carBrand.Id, carCategory.Id);
    }

    public CarModelRent AddCarModelRent(Guid id, DateTime validFrom, DateTime validUntil, decimal pricePerDay, decimal discount, bool isVisible)
    {
        var carModelRent = CarModelRent.Create(id, validFrom, validUntil, pricePerDay, isVisible, this, discount);

        _carModelRents.Add(carModelRent);
        return carModelRent;
    }

    public Result<bool> UpdateCarModelRent(Guid id, DateTime validFrom, DateTime validUntil, decimal pricePerDay, decimal discount, bool isVisible)
    {
        var carModelRent = _carModelRents.Where(x => x.Id == id).SingleOrDefault();
        if (carModelRent == null)
        {
            return Result.Failure<bool>(new Error(
                    "CarModelRents.NotFound",
                    $"The CarModelRent with Id: {id} doesn't exist"));
        }
        carModelRent.Update(validFrom, validUntil, pricePerDay, isVisible, discount);
        return true;
    }
}
