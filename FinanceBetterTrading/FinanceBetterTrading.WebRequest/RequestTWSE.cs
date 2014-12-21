
 using System;
using System.Collections.Generic;
﻿using System.Globalization;
﻿using System.Linq;
﻿using System.Runtime.Remoting.Messaging;
﻿using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
﻿using System.Web;
﻿using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Domain.Extension;
using HtmlAgilityPack;

namespace FinanceBetterTrading.WebRequest
{
    public class RequestTWSE:RequestServer
    {
        /// <summary>
        /// 將HTML股票資料解碼。
        /// 因為證交所是以月為鋹
        /// </summary>
        /// <returns></returns>
        private List<StockPriceInformation> DecodehtmlData(HtmlDocument htmlDocument)
        {
            List<StockPriceInformation> result = new List<StockPriceInformation>();
            string[] stockInformation = GetNameAndCode(htmlDocument);
            var pricehtml = htmlDocument.DocumentNode.ChildNodes;
            int count = 0;

            foreach (var item in pricehtml)
            {
                try
                {
                    if (item.Name == "tr")
                    {
                        count++;
                        if (count > 2)
                        {
                            StockPriceInformation stockPrice = new StockPriceInformation();
                            stockPrice.Name = stockInformation[2].Trim();
                            stockPrice.Code = stockInformation[1];
                            stockPrice.Date = ChangeDateFormate(item.SelectNodes("./td[1]")[0].InnerText);
                            stockPrice.TradeShare = item.SelectNodes("./td[2]")[0].InnerText.ParseThousandtoString();
                            stockPrice.TradeAmount = item.SelectNodes("./td[3]")[0].InnerText.ParseThousandtoString();
                            stockPrice.OpenPrice = float.Parse(item.SelectNodes("./td[4]")[0].InnerText.ParseSymbolsTostrZero());
                            stockPrice.HeightPrice = float.Parse(item.SelectNodes("./td[5]")[0].InnerText.ParseSymbolsTostrZero());
                            stockPrice.LowerPrice = float.Parse(item.SelectNodes("./td[6]")[0].InnerText.ParseSymbolsTostrZero());
                            stockPrice.ClosePrice = float.Parse(item.SelectNodes("./td[7]")[0].InnerText.ParseSymbolsTostrZero());
                            // stockPrice.PriceSpread = float.Parse(item.SelectNodes("./td[8]")[0].InnerText);
                            stockPrice.Volumn = item.SelectNodes("./td[9]")[0].InnerText.ParseThousandtoString();
                            result.Add(stockPrice);
                        }
                    }
                }
                catch (Exception e)
                {
                    var errordate = item.SelectNodes("./td[1]")[0].InnerText;
                    var error = ChangeDateFormate(item.SelectNodes("./td[1]")[0].InnerText);
                    throw e;
                }
         
            }
            return result;
        }

        /// <summary>
        /// 民國轉西元(請代入格式為:101/01/01類型)
        /// (想想有沒有更好的寫法)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string ChangeDateFormate(string date)
        {
            var temp = date.Split('/');
            var temp1 = Convert.ToInt16(temp[0]) + 1911;
            string result = date.Replace(temp[0], temp1.ToString());
            return result;
        }

        /// <summary>
        /// 抓取某支股票的全部股票。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<StockPriceInformation> GetStockPrice(string code)
        {
            List<StockPriceInformation> result = new List<StockPriceInformation>();
            DateTime date = DateTime.Now;
            string time = string.Empty;
            string year = string.Empty;
            string month = string.Empty;
            HtmlDocument gethtmldata = new HtmlDocument();
                    
            while (gethtmldata !=null)
            {              
                time = date.ToString("yyyyMM");
                year = date.ToString("yyyy");
                month = date.ToString("MM");
                string uri =
                    string.Format(
                        "http://www.twse.com.tw/ch/trading/exchange/STOCK_DAY/genpage/Report{0}/{0}_F3_1_8_{1}.php?STK_NO={1}&myear={2}&mmon={3}",
                        time, code, year, month);
                try
                {
                    //如果是網頁是用Post抓取資料的請改呼叫
                    // GetPostHtmlData(url, postdata, xpath);
                    //請看測試RequestServerTest裡的 TestPostdata() 
                    //Xpath要自已解析
                    gethtmldata = GetHtmlData(uri, "/html[1]/body[1]/table[1]/tr[3]/td[1]/table[3]");
                    if (gethtmldata != null)
                    {
                        var data = DecodehtmlData(gethtmldata);
                        result.AddRange(data);
                        result.Reverse();
                        date = date.AddMonths(-1);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }                                          
            }
            return result;
        }

        /// <summary>
        /// 取得股票名字與代碼
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        private string[] GetNameAndCode(HtmlDocument htmlDocument)
        {
            HtmlNodeCollection nodeHeader = htmlDocument.DocumentNode.SelectNodes("/tr[1]/td[1]/div[1]");
            string[] result = Regex.Split(nodeHeader[0].InnerText, "&nbsp;", RegexOptions.IgnoreCase);
            return result;
        }      
    }
}

