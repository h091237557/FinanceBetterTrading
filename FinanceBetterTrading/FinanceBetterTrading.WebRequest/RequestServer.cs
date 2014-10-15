using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Domain.Extension;
using HtmlAgilityPack;

namespace FinanceBetterTrading.WebRequest
{
    public class RequestServer
    {
        public HtmlDocument GetData(string url, string domtrace)
        {
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(url));

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.Default);
            // 裝載第一層查詢結果 
            HtmlDocument docStockContext = new HtmlDocument();
            docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode(domtrace).InnerHtml);
            ms.Close();
            return docStockContext;
        }

        /// <summary>
        /// 取得該月股價資訊
        /// 因為證交所是以月為鋹
        /// </summary>
        /// <returns></returns>
        public List<StockPriceInformation> GetStockPriceInformationFromTwse(HtmlDocument htmlDocument)
        {
            List<StockPriceInformation> result = new List<StockPriceInformation>();
            HtmlNodeCollection nodeHeader = htmlDocument.DocumentNode.SelectNodes("/tr[1]/td[1]/div[1]");
            var pricehtml = htmlDocument.DocumentNode.ChildNodes;
            int count = 0;
            foreach (var item in pricehtml)
            {
                if (item.Name == "tr")
                {
                    if (count == 0)
                    {
                        
                    }
                    
                    if (count > 2)
                    {
                        StockPriceInformation stockPrice = new StockPriceInformation();
                        stockPrice.Date = item.SelectNodes("./td[1]")[0].InnerText;
                        stockPrice.TradeShare = item.SelectNodes("./td[2]")[0].InnerText.ParseThousandthString();
                        stockPrice.TradeAmount = item.SelectNodes("./td[3]")[0].InnerText.ParseThousandthString();
                        stockPrice.OpenPrice = float.Parse(item.SelectNodes("./td[4]")[0].InnerText);
                        stockPrice.HeightPrice = float.Parse(item.SelectNodes("./td[5]")[0].InnerText);
                        stockPrice.LowerPrice = float.Parse(item.SelectNodes("./td[6]")[0].InnerText);
                        stockPrice.ClosePrice = float.Parse(item.SelectNodes("./td[7]")[0].InnerText);
                        stockPrice.PriceSpread = float.Parse(item.SelectNodes("./td[8]")[0].InnerText);
                        stockPrice.Volumn = item.SelectNodes("./td[9]")[0].InnerText.ParseThousandthString();
                        result.Add(stockPrice);
                    }
                }
            }
            return result;
        }

  
    }
}