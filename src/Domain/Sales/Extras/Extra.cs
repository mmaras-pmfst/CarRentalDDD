using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Extras;

public sealed class Extra : AggregateRoot, IAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal PricePerDay { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private Extra()
    {

    }

    private Extra(Guid id, string name, string description, decimal pricePerDay)
        : base(id)
    {
        Name = name;
        Description = description;
        PricePerDay = pricePerDay;
    }

    public static Extra Create(Guid id, string name, string description, decimal pricePerDay)
    {
        return new Extra(id, name, description, pricePerDay);
    }

    public void Update( string description, decimal pricePerDay)
    {
        Description = description;
        PricePerDay = pricePerDay;
    }

}
