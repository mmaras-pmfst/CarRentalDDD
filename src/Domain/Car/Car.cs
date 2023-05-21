using Domain.Car.Entities;
using Domain.Common.Models;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car;

public sealed class Car : AggregateRoot
{
    private readonly List<Contract> _reservations = new();
    public string NumberPlate { get; private set; }
    public string Name { get; private set; }
    public string Kilometers { get; private set; }
    public string Image { get; private set; }
    public decimal PricePerDay { get; private set; } // u model ili ovdje ili u category???
    public CarStatus Status { get; private set; }
    public FuelType FuelType { get; private set; }
    public Guid ColorId { get; private set; }
    public Guid CarModelId { get; private set; }
    public Guid OfficeId { get; private set; }

    public IReadOnlyCollection<Contract> Reservations => _reservations;


    private Car(Guid id, string numberPlate, string name, string kilometers, string image, decimal pricePerDay, CarStatus status, FuelType fuelType, Guid colorId, Guid carModelId, Guid officeId)
        : base(id)
    {
        NumberPlate = numberPlate;
        Name = name;
        Kilometers = kilometers;
        Image = image;
        PricePerDay = pricePerDay;
        Status = status;
        FuelType = fuelType;
        ColorId = colorId;
        CarModelId = carModelId;
        OfficeId = officeId;
    }

    private Car()
    {

    }

    public static Car Create(Guid id, string numberPlate, string name, string kilometers, string image, decimal pricePerDay, CarStatus status, FuelType fuelType, Guid colorId, Guid carModelId, Guid officeId)
    {
        return new Car(id, numberPlate, name, kilometers, image, pricePerDay, status, fuelType, colorId, carModelId, officeId);
    }
}
