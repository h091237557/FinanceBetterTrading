using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceBetterTrading.WebRequest;
using HtmlAgilityPack;
using System.Xml;

namespace FinanceBetterTrading.UITest.WebRequestTests
{
    [TestClass]
    public class RequstServer
    {
        [TestMethod]
        public void TestGetData()
        {
            RequestServer requestServer = new RequestServer();
            string url = "http://www.twse.com.tw/ch/trading/exchange/STOCK_DAY/genpage/Report201406/201406_F3_1_8_3514.php?STK_NO=3514&myear=2014&mmon=06";
            string domtrace = "/html[1]/body[1]/table[1]/tr[3]/td[1]/table[3]";
            var test = requestServer.GetData(url,domtrace);
            requestServer.GetStockPriceInformationFromTwse(test);
            Assert.AreEqual("1","2");
        }
    }
}
