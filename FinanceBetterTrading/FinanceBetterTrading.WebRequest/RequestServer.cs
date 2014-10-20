using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Domain.Extension;
using HtmlAgilityPack;

namespace FinanceBetterTrading.WebRequest
{
    public class RequestServer
    {
        /// <summary>
        /// 取得某網頁的資料
        /// </summary>
        /// <param name="url"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public HtmlDocument GetHtmlData(string url, string xPath)
        {
            WebClient client = new WebClient();
            MemoryStream ms = new MemoryStream(client.DownloadData(url));

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.Default);

            // 裝載第一層查詢結果 
            HtmlDocument docStockContext = new HtmlDocument();
            docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode(xPath).InnerHtml);
            ms.Close();
            return docStockContext;
        }       
    }
}