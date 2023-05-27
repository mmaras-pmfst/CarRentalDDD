using Domain.CarBrand.Entities;
using Domain.CarCategory;
using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand;

public sealed class CarBrand : AggregateRoot
{
    private readonly List<CarModel> _carModels = new();

    public string CarBrandName { get; private set; }
    public IReadOnlyCollection<CarModel> CarModels => _carModels;

    private CarBrand(Guid id, string carBrandName)
        : base(id)
    {
        CarBrandName = carBrandName;
    }
    private CarBrand() { }

    public static CarBrand Create(Guid id, string carBrandName)
    {
        return new CarBrand(id, carBrandName);
    }

    public void Update(string carBrandName)
    {
        CarBrandName = carBrandName;
    }

    public CarModel CreateCarModel(string carModelName, decimal basePricePerDay, CarCategory.CarCategory carCategory)
    {
        var carModel = new CarModel(Guid.NewGuid(), carModelName,basePricePerDay, this, carCategory);
        _carModels.Add(carModel);
        return carModel;
    }

    public CarModel UpdateCarModel(Guid carModelId, string carModelName, decimal basePricePerDay, CarCategory.CarCategory carCategory)
    {
        _carModels.RemoveAll(x => x.Id == carModelId);
        var carModel = new CarModel(carModelId, carModelName, basePricePerDay, this, carCategory);
        _carModels.Add(carModel);
        return carModel;
    }

    public ReservationContract CreateReservation(Guid carModelId, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId)
    {
        var carModel = _carModels.Where(x => x.Id == carModelId).FirstOrDefault();
        var reservation = carModel!.AddReservation(carModel, pickUpDate, dropDownDate, pickUpLocationId, dropDownLocationId);

        return reservation;
    }
}
