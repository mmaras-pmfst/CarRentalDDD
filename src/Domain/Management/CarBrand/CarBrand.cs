using Domain.Common.Models;
using Domain.Errors;
using Domain.Management.CarBrand.Entities;
using Domain.Management.CarBrand.ValueObjects;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrand;

public sealed class CarBrand : AggregateRoot
{
    private readonly List<CarModel> _carModels = new();

    public CarBrandName Name { get; private set; }
    public IReadOnlyCollection<CarModel> CarModels => _carModels;

    private CarBrand(Guid id, CarBrandName name)
        : base(id)
    {
        Name = name;
    }
    private CarBrand() { }

    public static CarBrand Create(Guid id, CarBrandName name)
    {
        
        return new CarBrand(id, name);
    }

    public Result Update(CarBrandName name)
    {
        
        Name = name;

        return Result.Success();
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

    #region Rules

    public const int NameMaxLength = 30;
    public const int NameMinLength = 2;

    #endregion
}
