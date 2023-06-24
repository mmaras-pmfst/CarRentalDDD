using Domain.Common.Models;
using Domain.Management.CarBrand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand;

public sealed class CarBrand : AggregateRoot
{
    private readonly List<CarModel> _carModels = new();

    public string Name { get; private set; }
    public IReadOnlyCollection<CarModel> CarModels => _carModels;

    private CarBrand(Guid id, string name)
        : base(id)
    {
        Name = name;
    }
    private CarBrand() { }

    public static CarBrand Create(Guid id, string name)
    {
        return new CarBrand(id, name);
    }

    public void Update(string name)
    {
        Name = name;
    }

    public CarModel CreateCarModel(string carModelName, CarCategory.CarCategory carCategory)
    {
        var carModel = CarModel.Create(Guid.NewGuid(), carModelName, this, carCategory);
        _carModels.Add(carModel);
        return carModel;
    }

    public CarModel UpdateCarModel(Guid carModelId, string carModelName, CarCategory.CarCategory carCategory)
    {
        var carModel = _carModels.FirstOrDefault(x => x.Id == carModelId);
        if(carModel == null)
        {
            return null;
        }
        carModel.Update(carModelName, carCategory);
        return carModel;
    }
}
