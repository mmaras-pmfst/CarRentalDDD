using Domain.Common.Models;
using Domain.Sales.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sales.Contract.Entities;
public sealed class ContractDetail : Entity
{
    public decimal Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Guid ExtrasId { get; private set; }
    public Guid ContractId { get; private set; }

    private ContractDetail()
    {

    }
    private ContractDetail(Guid id, decimal quantity, decimal price, Guid extrasId, Guid contractId)
        :base(id)
    {
        Quantity = quantity;
        Price = price;
        ExtrasId = extrasId;
        ContractId = contractId;
    }

    public static ContractDetail Create(Guid id, decimal quantity, Extras.Extra extras, Contract contract)
    {
        var duration = (decimal)contract.DropDownDate.Subtract(contract.PickUpDate).TotalDays;

        var price = duration * extras.PricePerDay*quantity;

        return new ContractDetail(id, quantity, price, extras.Id, contract.Id);
    }

}
