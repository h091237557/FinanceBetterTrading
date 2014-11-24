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
        public void TestGetStockPrice()
        {
            RequestTWSE requestTwse = new RequestTWSE();
            var result = requestTwse.GetStockPrice("3514");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestGetStockPriceIsNull()
        {
            RequestTWSE requestTwse = new RequestTWSE();
            var result = requestTwse.GetStockPrice("0000");

            var expect = 0;
            var actual = result.Count;
            Assert.AreEqual(expect,actual);
        }
    }
}
