using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceBetterTrading.Domain
{
    public class StockPriceInformation
    {
        public string Id { get; set; }
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
    public class InstitutionalInvestorsScheduleDataInformation
    {
        public string Date { get; set; }
        public string StockCode { get; set; }
        public int ForeignCapitalBuyShares { get; set; }
        public int ForeignCapitalSellShares { get; set; }
        //public int ForeignCapitalBuySellShares { get; set; }
        public int InvestmentTrustBuyShares { get; set; }
        public int InvestmentTrustSellShares { get; set; }
        //public int InvestmentTrustBuySellShares { get; set; }
        public int DealerBuySharesProprietaryTrading { get; set; }
        public int DealerSellSharesProprietaryTrading { get; set; }
        //public int DealerBuySellSharesProprietaryTrading { get; set; }
        public int DealerBuySharesHedge { get; set; }
        public int DealerSellSharesHedge { get; set; }
        //public int DealerBuySellSharesHedge { get; set; }
        public int InstitutionalInvestorsBuySellShares { get; set; }
    }
}
