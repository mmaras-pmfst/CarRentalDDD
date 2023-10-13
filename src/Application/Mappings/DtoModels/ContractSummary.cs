using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings.DtoModels;
public class ContractSummary
{
    public Guid ContractId { get; set; }
    public decimal RentalPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
