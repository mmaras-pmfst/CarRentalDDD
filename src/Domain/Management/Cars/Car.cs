using Domain.Common.Models;
using Domain.Management.CarModels;
using Domain.Management.Offices;
using Domain.Sales.Contracts;
using Domain.Shared;
using Domain.Shared.Enums;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Cars;

public sealed class Car : AggregateRoot
{
    public string NumberPlate { get; private set; }
    public decimal Kilometers { get; private set; }
    public byte[]? Image { get; private set; }
    public CarStatus Status { get; private set; }
    public FuelType FuelType { get; private set; }
    public Guid CarModelId { get; private set; }
    public Guid OfficeId { get; private set; }

    public CarModel CarModel { get; private set; }
    public Office Office { get; private set; }
    public IReadOnlyCollection<Contract> Contracts { get; private set; }



    private Car(Guid id, string numberPlate, decimal kilometers, byte[]? image, CarStatus status, FuelType fuelType, Guid carModelId, Guid officeId)
        : base(id)
    {
        NumberPlate = numberPlate;
        Kilometers = kilometers;
        Image = image;
        Status = status;
        FuelType = fuelType;
        CarModelId = carModelId;
        OfficeId = officeId;
    }
    private Car()
    {
    }

    public static Car Create(Guid id, string numberPlate, decimal kilometers, byte[]? image, CarStatus status, FuelType fuelType, CarModel carModel, Office office)
    {
        return new Car(id, numberPlate, kilometers, image, status, fuelType, carModel.Id, office.Id);
    }

    public Result<bool> Update(decimal kilometers, byte[]? image, CarStatus status, Office office)
    {

        Image = image;
        Status = status;
        OfficeId = office.Id;
        if (Kilometers <= kilometers)
        {
            Kilometers = kilometers;
            return Result.Failure<bool>(new Error(
                    "Car.KilometersError",
                    $"The Car kilometers cannot be less then current"));
        }

        return Result.Success<bool>(true);
    }
}
