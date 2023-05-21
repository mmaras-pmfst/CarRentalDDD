using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CarBrand.Entities;

public sealed class Reservation : Entity
{
    public DateTime PickUpDate { get; private set; }
    public DateTime DropDownDate { get; private set; }
    public decimal TotalPrice { get; private set; }
    public Guid CarModelId { get; private set; }
    public Guid PickUpLocationId { get; private set; }
    public Guid DropDownLocationId { get; private set; }

    internal Reservation(Guid id, DateTime pickUpDate, DateTime dropDownDate, CarModel carModel, Guid pickUpLocationId, Guid dropDownLocationId)
        :base(id)
    {
        {
            
}
        var duration = (decimal)dropDownDate.Subtract(pickUpDate).TotalDays;
        PickUpDate = pickUpDate;
        DropDownDate = dropDownDate;
        TotalPrice = duration * carModel.BasePricePerDay;
        CarModelId = carModel.Id;
        PickUpLocationId = pickUpLocationId;
        DropDownLocationId = dropDownLocationId;
    }

    private Reservation()
    {
    }

}
