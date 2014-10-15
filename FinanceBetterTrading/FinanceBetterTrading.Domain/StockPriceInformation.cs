using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceBetterTrading.Domain
{
    public class StockPriceInformation
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public float OpenPrice { get; set; }
        public float ClosePrice { get; set; }
        public float HeightPrice { get; set; }
        public float LowerPrice { get; set; }
        public int Volumn { get; set; }
        public int TradeAmount { get; set; }
        public int TradeShare { get; set; }
        public float PriceSpread { get; set; }
    }
}
