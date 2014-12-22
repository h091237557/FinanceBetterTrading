using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.WebRequest;
using log4net;

namespace FinanceBetterTrading.Service
{
    public class StockPriceService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(StockPriceService));
        public List<StockPrice> GetStockPriceByCode(string code)
        {
            StockPriceDal StockPriceDal = new StockPriceDal();
            try
            {
                StockPriceDal.Open(DBConn.Conn);
                var result = StockPriceDal.SelectByCode(code);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,e);
                throw e;
            }
            finally
            {
                if (StockPriceDal.Connection != null)
                    StockPriceDal.Connection.Close();
            }
        }
    }
}
