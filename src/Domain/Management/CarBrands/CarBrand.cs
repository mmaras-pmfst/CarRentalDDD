using Domain.Common.Models;
using Domain.Management.CarBrands.ValueObjects;
using Domain.Management.CarModels;
using Domain.Sales.Contracts.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Management.CarBrands;

public sealed class CarBrand : AggregateRoot, IAuditableEntity
{
    public CarBrandName Name { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public IReadOnlyCollection<CarModel> CarModels { get; private set; }


    public CarBrand(Guid id, CarBrandName name)
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


}
