using Domain.Common.Models;
using Domain.Enums;
using Domain.Shared;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.Car;

public sealed class Car : AggregateRoot
{
    private readonly List<Guid> _contracts = new();
    public string NumberPlate { get; private set; }
    public string Name { get; private set; }
    public decimal Kilometers { get; private set; }
    public byte[]? Image { get; private set; }
    public CarStatus Status { get; private set; }
    public FuelType FuelType { get; private set; }
    public Guid ColorId { get; private set; }
    public Guid CarModelId { get; private set; }
    public Guid OfficeId { get; private set; }

    public IReadOnlyList<Guid> Contracts => _contracts;


    private Car(Guid id, string numberPlate, string name, decimal kilometers, byte[]? image, CarStatus status, FuelType fuelType, Guid colorId, Guid carModelId, Guid officeId)
        : base(id)
    {
        NumberPlate = numberPlate;
        Name = name;
        Kilometers = kilometers;
        Image = image;
        Status = status;
        FuelType = fuelType;
        ColorId = colorId;
        CarModelId = carModelId;
        OfficeId = officeId;
    }
    private Car()
    {
    }

    public static Car Create(Guid id, string numberPlate, string name, decimal kilometers, byte[]? image, CarStatus status, FuelType fuelType, Guid colorId, Guid carModelId, Guid officeId)
    {
        return new Car(id, numberPlate, name, kilometers, image, status, fuelType, colorId, carModelId, officeId);
    }

    public Result<bool> Update(decimal kilometers, byte[]? image, CarStatus status, Guid officeId)
    {
        
        Image = image;
        Status = status;
        OfficeId = officeId;
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
