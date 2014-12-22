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
            if (y == "w")
            {
                InsertInstitutionalInvestorsScheduleData();;
            }

        }

        private static void InsertStockPrice()
        {
            RequestTWSE requestTwse = new RequestTWSE();
            StockDAL stockDal = new StockDAL();
            StockPriceDal stockPriceDal = new StockPriceDal();
            try
            {
                stockDal.Open(DBConn.Conn);
                stockPriceDal.Connection = stockDal.Connection;
                var allStock = stockDal.SelectAll();

                foreach (var stock in allStock)
                {
                    Console.WriteLine("{0} 開始進行", stock.Code);
                    var stockPrice = requestTwse.GetStockPrice(stock.Code);
                    Console.WriteLine("{0} 成功從TWSE抓取", stock.Code);
                    stockPriceDal.InsertBatch(stockPrice);
                    Console.WriteLine("{0} 成功匯入", stock.Code);
                    stockDal.UpdateIsGetHistoryPrice(stock.Code);
                }

            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw e;
            }
            finally
            {
                if (stockPriceDal.Connection != null)
                    stockPriceDal.Connection.Close();
            }
        }

        private static void InsertInstitutionalInvestorsScheduleData()
        {
            RequestTWSE requestTwse = new RequestTWSE();
            InstitutionalInvestorsInformationDAL InstitutionalInvestorsInformationDal = new InstitutionalInvestorsInformationDAL();

            //var result = requestTwse.GetInstitutionalInvestorsScheduleData();

            try
            {
                InstitutionalInvestorsInformationDal.Open(DBConn.Conn);
                //InstitutionalInvestorsInformationDal.InsertBatch(result);
                var InstitutionalInvestorsList = requestTwse.GetInstitutionalInvestorsScheduleData("103/12/19");
                InstitutionalInvestorsInformationDal.InsertBatch(InstitutionalInvestorsList);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (InstitutionalInvestorsInformationDal.Connection != null)
                    InstitutionalInvestorsInformationDal.Connection.Close();
            }
        }
    }
}
