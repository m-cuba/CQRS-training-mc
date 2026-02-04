using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportsbook.ServiceTemplate.Application.Commands.CreateItem
{
    public record CreateItemCommand(string Sku, string Name);

}
