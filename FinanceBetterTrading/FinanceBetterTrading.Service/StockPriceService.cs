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
        public List<StockPriceInformation> GetStockPriceByCode(string code)
        {
            StockPriceInformationDal stockPriceInformationDal = new StockPriceInformationDal();
            try
            {
                stockPriceInformationDal.Open(DBConn.Conn);
                var result = stockPriceInformationDal.SelectByCode(code);
                return result;
            }
            catch (Exception e)
            {
                Logger.Error(e,e);
                throw e;
            }
            finally
            {
                if (stockPriceInformationDal.Connection != null)
                    stockPriceInformationDal.Connection.Close();
            }
        }
    }
}
