using System;
using System.Collections.Generic;
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
    public class StockPriceDALTest
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
            StockPriceDal StockPriceDal = new StockPriceDal();
            try
            {
                using (var scrop = new TransactionScope())
                {
                    StockPriceDal.Open(DBConn.Conn);
                    var stock = CreateStockObject();
                    StockPriceDal.Insert(stock);

                    var actual = StockPriceDal.Select(stock.Id).Name;
                    var expect = stock.Name;
                   
                    Assert.AreEqual(expect, actual);
                    //scrop.Complete();
                }                
            }
            finally
            {
                if (StockPriceDal.Connection != null)
                    StockPriceDal.Connection.Close();
            }
        }

        [TestMethod]
        public void TestInsertBatch()
        {
            StockPriceDal StockPriceDal = new StockPriceDal();
            try
            {
                using (var scrop = new TransactionScope())
                {
                    StockPriceDal.Open(DBConn.Conn);
                    List<StockPrice> stocks = new List<StockPrice>();
                    stocks.Add(CreateStockObject());
                    stocks.Add(CreateStockObject());

                    StockPriceDal.InsertBatch(stocks);

                    var actual = StockPriceDal.Select(stocks[0].Id).Name;
                    var expect = stocks[0].Name;

                    Assert.AreEqual(expect, actual);                  
                }
            }
            finally
            {
                if (StockPriceDal.Connection != null)
                    StockPriceDal.Connection.Close();
            }
        }

        [TestMethod]
        public void TestDelete()
        {
            StockPriceDal StockPriceDal = new StockPriceDal();
            try
            {
                using (new TransactionScope())
                {
                    StockPriceDal.Open(DBConn.Conn);
                    var stock = CreateStockObject();
                    StockPriceDal.Insert(stock);

                    var actual = StockPriceDal.Select(stock.Id).Name;
                    var expect = stock.Name;
                    Assert.AreEqual(expect, actual);

                    StockPriceDal.Delete(stock);

                    var actualcancel = StockPriceDal.Select(stock.Id).Name;
                    Assert.IsNull(actualcancel);                 
                }
            }
            finally
            {
                if (StockPriceDal.Connection != null)
                    StockPriceDal.Connection.Close();
            }
        }

        private StockPrice CreateStockObject()
        {
            StockPrice stock = new StockPrice();
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
