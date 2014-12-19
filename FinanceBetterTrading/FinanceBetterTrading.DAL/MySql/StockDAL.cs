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
        public List<StockInformation> SelectAll()
        {
            List<StockInformation> result = new List<StockInformation>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM stock";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                StockInformation stock = new StockInformation();
                ReadAll(reader,stock);
                result.Add(stock);
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 將從資料庫中讀取出的資料存放入stock物件裡。
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="stock"></param>
        public void ReadAll(MySqlDataReader reader , StockInformation stock)
        {
            stock.Code = reader["Code"].ToString();
        }
    }
}
