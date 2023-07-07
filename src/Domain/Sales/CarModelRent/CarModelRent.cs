using Domain.Common.Models;
using Domain.Management.CarBrand.Entities;
using Domain.Sales.CarModelRent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.CarModelRent;
public sealed class CarModelRent : Entity
{
    private readonly List<Reservation> _reservations = new();

    public DateTime ValidFrom { get; private set; }
    public DateTime? ValidUntil { get; private set; }
    public decimal PricePerDay { get; private set; }
    public decimal Discount { get; private set; }
    public bool IsVisible { get; private set; }
    public Guid CarModelId { get; private set; }

    public IReadOnlyCollection<Reservation> Reservations => _reservations;


    private CarModelRent()
    {

    }

    private CarModelRent(Guid id, DateTime validFrom, DateTime? validUntil, decimal pricePerDay, bool isVisible, CarModel carModel, decimal discount)
        : base(id)
    {
        ValidFrom = validFrom;
        ValidUntil = validUntil;
        PricePerDay = pricePerDay;
        IsVisible = isVisible;
        CarModelId = carModel.Id;
        Discount = discount;
    }

    public static CarModelRent Create(Guid id, DateTime validFrom, DateTime? validUntil, decimal pricePerDay, bool isVisible, CarModel carModel, decimal discount)
    {
        return new CarModelRent(id, validFrom, validUntil, pricePerDay, isVisible, carModel, discount);
    }

    public void Update(DateTime validFrom, DateTime? validUntil, decimal pricePerDay, bool isVisible, decimal discount)
    {
        ValidFrom = validFrom;
        ValidUntil = validUntil;
        PricePerDay = pricePerDay;
        IsVisible = isVisible;
        Discount= discount;
    }


}
