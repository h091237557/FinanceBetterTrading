using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using MySql.Data.MySqlClient;

namespace FinanceBetterTrading.DAL.MySql
{
    public class StockDAL:MySqlDal
    {
        /// <summary>
        /// 抓取全部的資料
        /// </summary>
        /// <returns></returns>
        public List<Stock> SelectAll()
        {
            List<Stock> result = new List<Stock>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock WHERE IsGetHistoryPrice=false";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Stock stock = new Stock();
                ReadAll(reader,stock);
                result.Add(stock);
            }
            reader.Close();
            return result;
        }


        /// <summary>
        /// 記錄某支股票已抓取歷史股價
        /// </summary>
        public void UpdateIsGetHistoryPrice(string code)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE stock SET IsGetHistoryPrice=true WHERE Code=?code";
            command.Parameters.AddWithValue("?code", code);
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// 將從資料庫中讀取出的資料存放入stock物件裡。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="stock"></param>
        public void ReadAll(MySqlDataReader reader , Stock stock)
        {
            stock.Code = reader["Code"].ToString();
        }
    }
}
