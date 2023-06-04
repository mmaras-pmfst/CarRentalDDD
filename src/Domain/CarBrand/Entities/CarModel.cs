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
    public string CarModelName { get; private set; }
    public decimal BasePricePerDay { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }
    public IReadOnlyList<Guid> CarIds => _carIds.ToList();

    internal CarModel(Guid id, string carModelName, decimal basePricePerDay, CarBrand carBrand, CarCategory.CarCategory carCategory)
        :base(id)
    {
        CarModelName = carModelName;
        BasePricePerDay= basePricePerDay;
        CarBrandId = carBrand.Id;
        CarCategoryId = carCategory.Id;
    }
    private CarModel() { }


    public void Update(string carModelName, decimal basePricePerDay, CarCategory.CarCategory carCategory)
    {
        CarModelName = carModelName;
        BasePricePerDay = basePricePerDay;
        CarCategoryId= carCategory.Id;
    }
}
