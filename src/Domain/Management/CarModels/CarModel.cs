using Domain.Common.Models;
using Domain.Management.CarBrands;
using Domain.Management.CarCategories;
using Domain.Management.CarModels.ValueObjects;
using Domain.Management.Cars;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarModels;

public sealed class CarModel : AggregateRoot, IAuditableEntity
{
    public CarModelName Name { get; private set; }
    public decimal PricePerDay { get; private set; }
    public decimal Discount { get; private set; }
    public Guid CarBrandId { get; private set; }
    public Guid CarCategoryId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public CarBrand CarBrand { get; private set; }
    public CarCategory CarCategory { get; private set; }
    public IReadOnlyCollection<Car> Cars { get; private set; }


    internal CarModel(Guid id, CarModelName name, Guid carBrandId, Guid carCategoryId, decimal pricePerDay, decimal discount)
        : base(id)
    {
        Name = name;
        CarBrandId = carBrandId;
        CarCategoryId = carCategoryId;
        PricePerDay = pricePerDay;
        Discount = discount;
    }
    private CarModel() { }

    public void Update(CarModelName name, CarCategory carCategory)
    {
        Name = name;
        CarCategoryId = carCategory.Id;
    }

    public static CarModel Create(Guid id, CarModelName name, CarBrand carBrand, CarCategory carCategory, decimal pricePerDay, decimal discount)
    {
        return new CarModel(id, name, carBrand.Id, carCategory.Id, pricePerDay, discount);
    }

}
