using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Domain.Extension;
using HtmlAgilityPack;

namespace FinanceBetterTrading.WebRequest
{
    public class RequestTWSE:RequestServer
    {
        /// <summary>
        /// 取得該月股價資訊(TWSE證交所所)
        /// 因為證交所是以月為鋹
        /// </summary>
        /// <returns></returns>
        public List<StockPriceInformation> GetStockPriceInformation(HtmlDocument htmlDocument)
        {
            List<StockPriceInformation> result = new List<StockPriceInformation>();
            string[] stockInformation = GetNameAndCode(htmlDocument);
            var pricehtml = htmlDocument.DocumentNode.ChildNodes;
            int count = 0;
            foreach (var item in pricehtml)
            {
                if (item.Name == "tr")
                {
                    count++;
                    if (count > 2)
                    {
                        StockPriceInformation stockPrice = new StockPriceInformation();
                        stockPrice.Name = stockInformation[2].Trim();
                        stockPrice.Code = stockInformation[1];
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

        private string[] GetNameAndCode(HtmlDocument htmlDocument)
        {
            HtmlNodeCollection nodeHeader = htmlDocument.DocumentNode.SelectNodes("/tr[1]/td[1]/div[1]");
            string[] result = Regex.Split(nodeHeader[0].InnerText, "&nbsp;", RegexOptions.IgnoreCase);
            return result;
        }
    }
}
