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
            string url =
                "http://www.twse.com.tw/ch/trading/exchange/STOCK_DAY/genpage/Report201406/201406_F3_1_8_3514.php?STK_NO=3514&myear=2014&mmon=06";
           
            var test = requestServer.GetData(url);

            Assert.AreEqual("1","2");
        }
    }
}
