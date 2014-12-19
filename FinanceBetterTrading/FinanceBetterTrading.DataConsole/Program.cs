using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.DAL.MySql;
using FinanceBetterTrading.Service;
using FinanceBetterTrading.Web.App_Start;
using FinanceBetterTrading.WebRequest;
using System.Transactions;
using log4net;

namespace FinanceBetterTrading.DataConsole
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            DBConfig.Register();
            Console.WriteLine("Execute? Press y Test? Press t");           
            var y = Console.ReadLine();
            if (y == "y")
            {
                InsertStockPrice();;
            }
        }

        private static void InsertStockPrice()
        {
            RequestTWSE requestTwse = new RequestTWSE();
            StockPriceInformationDal stockPriceInformationDal = new StockPriceInformationDal();
            StockDAL stockDal = new StockDAL();

            try
            {

                stockPriceInformationDal.Open(DBConn.Conn);
                stockDal.Connection = stockPriceInformationDal.Connection;
                var allStock = stockDal.SelectAll();

                foreach (var stock in allStock)
                {
                    var stockPrice = requestTwse.GetStockPrice(stock.Code);
                    Console.WriteLine("{0} 成功從TWSE抓取", stock.Code);
                    stockPriceInformationDal.InsertBatch(stockPrice);
                    Console.WriteLine("{0} 成功匯入", stock.Code);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e);
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
