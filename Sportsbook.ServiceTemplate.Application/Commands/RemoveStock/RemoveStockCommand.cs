using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportsbook.ServiceTemplate.Application.Commands.RemoveStock
{
    public record RemoveStockCommand(string Sku, int Quantity);

}
