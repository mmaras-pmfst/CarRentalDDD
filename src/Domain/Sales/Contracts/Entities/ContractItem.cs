using Domain.Common.Models;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Contracts.Entities;

public sealed class ContractItem : Entity
{
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Guid ExtraId { get; private set; }
    public Guid ContractId { get; private set; }

    private ContractItem()
    {

    }
    private ContractItem(Guid id, decimal quantity, decimal price, Guid extraId, Guid contractId)
        : base(id)
    {
        Quantity = quantity;
        Price = price;
        ContractId = contractId;
        ExtraId= extraId;
    }

    public static ContractItem Create(Guid id, decimal quantity, Extra extra, Contract contract)
    {
        var duration = (decimal)contract.DropDownDate.Subtract(contract.PickUpDate).TotalDays;

        var price = duration * extra.PricePerDay * quantity;

        return new ContractItem(id, quantity, price, extra.Id, contract.Id);
    }

}
