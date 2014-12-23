using System;
using System.Globalization;
using System.Text;
using System.Web;
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
    }
}
