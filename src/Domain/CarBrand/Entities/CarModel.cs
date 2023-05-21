using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand.Entities;

public sealed class CarModel : Entity
{
    private readonly HashSet<Guid> _carIds = new();
    private readonly List<Reservation> _reservations = new();
    public string CarModelName { get; private set; }
    public decimal BasePricePerDay { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }
    public IReadOnlyList<Guid> CarIds => _carIds.ToList();
    public IReadOnlyCollection<Reservation> Reservations=> _reservations;

    internal CarModel(Guid id, string carModelName, decimal basePricePerDay, CarBrand carBrand, CarCategory.CarCategory carCategory)
        :base(id)
    {
        CarModelName = carModelName;
        BasePricePerDay= basePricePerDay;
        CarBrandId = carBrand.Id;
        CarCategoryId = carCategory.Id;
    }
    private CarModel() { }

    public Reservation AddReservation(CarModel carModel, DateTime pickUpDate, DateTime dropDownDate, Guid pickUpLocationId, Guid dropDownLocationId)
    {
        var reservation = new Reservation(Guid.NewGuid(), pickUpDate, dropDownDate, carModel , pickUpLocationId, dropDownLocationId);
        _reservations.Add(reservation);
        return reservation;
    }
}
