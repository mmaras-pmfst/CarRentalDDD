using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Extras;
public sealed class Extras : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal PricePerDay { get; private set; }

    private Extras()
    {

    }

    private Extras(Guid id, string name, string description, decimal pricePerDay)
        :base(id)
    {
        Name = name;
        Description = description;
        PricePerDay = pricePerDay;
    }

    public static Extras Create(Guid id, string name, string description, decimal pricePerDay)
    {
        return new Extras(id, name, description, pricePerDay);
    }

    public void Update(string name, string description, decimal pricePerDay)
    {
        Name= name;
        Description= description;
        PricePerDay= pricePerDay;
    }

}
