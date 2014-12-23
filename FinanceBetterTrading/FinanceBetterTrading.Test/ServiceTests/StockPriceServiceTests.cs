using System;
using System.Collections.Generic;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.DAL.MySql;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Service;
using FinanceBetterTrading.Web.App_Start;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace FinanceBetterTrading.Test.ServiceTests
{
    [Binding]
    public class StockPriceServiceTestsSteps
    {
        [BeforeScenario]
        public void BeforeScenario()
        {
            DBConfig.Register();
        }


        [Given(@"查詢條件股票代號為 (.*)")]
        public void Given查詢條件股票代號為(string code)
        {
            ScenarioContext.Current.Set<string>(code,"code");
        }

        [Given(@"預計stockprice資料表應有")]
        public void Given預計Stockprice資料表應有(Table table)
        {
            List<StockPrice> stocks = (List<StockPrice>) table.CreateSet<StockPrice>();
            StockPriceDal stockPriceDal = new StockPriceDal();
            try
            {
                stockPriceDal.Open(DBConn.Conn);
                stockPriceDal.InsertBatch(stocks);
            }
            finally
            {
                if (stockPriceDal.Connection != null)
                    stockPriceDal.Connection.Close();
            }
        }

        [When(@"呼叫查詢結果")]
        public void When呼叫查詢結果()
        {
           StockPriceService stockPriceService = new StockPriceService();
            var code = ScenarioContext.Current.Get<string>("code");
            var result = stockPriceService.GetStockPriceByCode(code);
            ScenarioContext.Current.Set<List<StockPrice>>(result,"actual");
        }

        [Then(@"查詢結果應為")]
        public void Then查詢結果應為(Table table)
        {
            var expect = (List<StockPrice>)table.CreateSet<StockPrice>();
            var actual = ScenarioContext.Current.Get<List<StockPrice>>("actual");
            Assert.AreEqual(expect.Count,actual.Count);
        }
    }
}
