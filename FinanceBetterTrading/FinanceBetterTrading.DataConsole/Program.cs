using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.Service;
using FinanceBetterTrading.Web.App_Start;
using FinanceBetterTrading.WebRequest;

namespace FinanceBetterTrading.DataConsole
{
    class Program
    {
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

            var result = requestTwse.GetStockPrice("3514");

            try
            {
                stockPriceInformationDal.Open(DBConn.Conn);
                stockPriceInformationDal.InsertBatch(result);
            }
            catch (Exception e)
            {              
                throw e;
            }
            finally
            {
                if(stockPriceInformationDal.Connection != null)
                    stockPriceInformationDal.Connection.Close(); 
            }
        }
    }
}
