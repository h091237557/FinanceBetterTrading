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
            // 裝載第一層查詢結果           
            try
            {
                HtmlDocument docStockContext = new HtmlDocument();
                WebClient client = new WebClient();
                MemoryStream ms = new MemoryStream(client.DownloadData(url));
                // 使用預設編碼讀入 HTML 
                HtmlDocument doc = new HtmlDocument();
                doc.Load(ms, Encoding.Default);
                if (doc.DocumentNode.InnerText.Contains("查無資料"))
                    return null;
                docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode(xPath).InnerHtml);
                ms.Close();
                return docStockContext;
            }
            catch (Exception)
            {
                return null;
            }           
        }

        public HtmlDocument GetPostHtmlData(string url, string formdata, string xPath)
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] postBytes = ascii.GetBytes(formdata);
            HttpWebRequest webRequest = (HttpWebRequest) System.Net.WebRequest.Create(url);
            HtmlDocument docStockContext = new HtmlDocument();
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postBytes.Length;

            Stream postStream = webRequest.GetRequestStream();
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Flush();
            postStream.Close();

            try
            {
                var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                Encoding encode = Encoding.GetEncoding("big5"); 
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream(),encode))
                {
                    HtmlDocument doc = new HtmlDocument();
                    doc.Load(streamReader.BaseStream, Encoding.Default);
                    if (doc.DocumentNode.InnerText.Contains("查無資料"))
                        return null;
                    docStockContext.LoadHtml(doc.DocumentNode.SelectSingleNode(xPath).InnerHtml);
                }
                httpResponse.Close();
                httpResponse.Dispose();
                return docStockContext;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}