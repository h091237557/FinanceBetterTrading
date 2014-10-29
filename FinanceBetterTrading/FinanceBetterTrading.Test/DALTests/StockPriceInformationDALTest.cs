using System;
using FinanceBetterTrading.Web.App_Start;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Service;
using System.Data;
using System.Transactions;


namespace FinanceBetterTrading.Test.DALTests
{
    [TestClass]
    public class StockPriceInformationDALTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            DBConfig.Register();
        }

        /// <summary>
        /// 測試Insert
        /// </summary>
        [TestMethod]
        public void TestInsert()
        {
            StockPriceInformationDal stockPriceInformationDal = new StockPriceInformationDal();
            try
            {
                using (var scrop = new TransactionScope())
                {
                    stockPriceInformationDal.Open(DBConn.Conn);
                    var stock = CreateStockObject();
                    stockPriceInformationDal.Insert(stock);

                    var actual = stockPriceInformationDal.Select(stock.Id).Name;
                    var expect = stock.Name;
                   
                    Assert.AreEqual(expect, actual);
                    //scrop.Complete();
                }                
            }
            finally
            {
                if (stockPriceInformationDal.Connection != null)
                    stockPriceInformationDal.Connection.Close();
            }
        }
        [TestMethod]
        public void TestDelete()
        {
            StockPriceInformationDal stockPriceInformationDal = new StockPriceInformationDal();
            try
            {
                using (new TransactionScope())
                {
                    stockPriceInformationDal.Open(DBConn.Conn);
                    var stock = CreateStockObject();
                    stockPriceInformationDal.Insert(stock);

                    var actual = stockPriceInformationDal.Select(stock.Id).Name;
                    var expect = stock.Name;
                    Assert.AreEqual(expect, actual);

                    stockPriceInformationDal.Delete(stock);

                    var actualcancel = stockPriceInformationDal.Select(stock.Id).Name;
                    Assert.IsNull(actualcancel);                 
                }
            }
            finally
            {
                if (stockPriceInformationDal.Connection != null)
                    stockPriceInformationDal.Connection.Close();
            }
        }

        private StockPriceInformation CreateStockObject()
        {
            StockPriceInformation stock = new StockPriceInformation();
            stock.Name = "Test";
            stock.Code = "1234";
            stock.Date = "103/10/01";
            stock.OpenPrice = float.Parse("26.50");
            stock.HeightPrice = float.Parse("26.50");
            stock.LowerPrice = float.Parse("26.50");
            stock.ClosePrice = float.Parse("26.50");
            stock.TradeAmount = 1342204;
            stock.TradeShare = 35817298;
            stock.PriceSpread = float.Parse(("-0.05"));
            stock.Volumn = 612;
            return stock;
        }
    }
}
