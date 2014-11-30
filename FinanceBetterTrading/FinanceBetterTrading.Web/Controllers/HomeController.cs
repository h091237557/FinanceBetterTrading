using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Antlr.Runtime;
using FinanceBetterTrading.Service;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Newtonsoft.Json;

namespace FinanceBetterTrading.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult GetStockPrice(string code)
        {         
            StockPriceService stockPriceService = new StockPriceService();
            var datas = stockPriceService.GetStockPriceByCode(code);
            var result = JsonConvert.SerializeObject(datas);
            return Content(result, "application/json"); 
        }
    }
}