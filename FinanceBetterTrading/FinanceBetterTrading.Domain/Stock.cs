using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FinanceBetterTrading.Domain
{
    public class Stock
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
