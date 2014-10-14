using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace FinanceBetterTrading.WebRequest
{
    public class RequestServer
    {

        public HtmlDocument GetData(string url)
        {
            // 下載 Yahoo 奇摩股市資料 (範例為 2317 鴻海) 
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(url));

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.Default);

            // 裝載第一層查詢結果 
            HtmlDocument docStockContext = new HtmlDocument();

            docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode(
        "/html[1]/body[1]/table[1]/tbody[1]/tr[3]/td[1]/table[3]/tbody[1]").InnerHtml);
            ms.Close();
            return docStockContext;

            // 取得個股標頭 
        //    HtmlNodeCollection nodeHeaders =
        // docStockContext.DocumentNode.SelectNodes("./tr[1]/th");
        //    // 取得個股數值 
        //    string[] values = docStockContext.DocumentNode.SelectSingleNode(
        //"./tr[2]").InnerText.Trim().Split('\n');
        //    int i = 0;

            // 輸出資料 
        //    foreach (HtmlNode nodeHeader in nodeHeaders)
        //    {
        //        Console.WriteLine("Header: {0}, Value: {1}",
        //nodeHeader.InnerText, values[i].Trim());
        //        i++;
        //    }

            
        }


    }
}
