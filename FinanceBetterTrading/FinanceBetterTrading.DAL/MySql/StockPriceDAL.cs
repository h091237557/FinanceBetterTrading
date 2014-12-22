using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using MySql.Data.MySqlClient;

namespace FinanceBetterTrading.DAL
{
    public class StockPriceDal:MySqlDal
    {
        /// <summary>
        /// 新增一筆資料進 StockPrice 資料表
        /// </summary>
        /// <param name="stock"></param>
        public void Insert(StockPrice stock)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO StockPrice ";
            command.CommandText +=
                "(Name,Code,Date,OpenPrice,ClosePrice,HeightPrice,LowerPrice,Volumn,TradeAmount,TradeShare,PriceSpread) ";
            command.CommandText += "VALUES (?Name, ?Code, ?Date, ?OpenPrice, ?ClosePrice, ?HeightPrice, ?LowerPrice, ?Volumn, ?TradeAmount, ?TradeShare,?PriceSpread)";
            command.Parameters.AddWithValue("?Name", stock.Name);
            command.Parameters.AddWithValue("?Code", stock.Code);
            command.Parameters.AddWithValue("?Date", stock.Date);
            command.Parameters.AddWithValue("?OpenPrice", stock.OpenPrice);
            command.Parameters.AddWithValue("?ClosePrice", stock.ClosePrice);
            command.Parameters.AddWithValue("?HeightPrice", stock.HeightPrice);
            command.Parameters.AddWithValue("?LowerPrice", stock.LowerPrice);
            command.Parameters.AddWithValue("?Volumn", stock.Volumn);
            command.Parameters.AddWithValue("?TradeAmount", stock.TradeAmount);
            command.Parameters.AddWithValue("?TradeShare", stock.TradeShare);
            command.Parameters.AddWithValue("?PriceSpread", stock.PriceSpread);
            command.ExecuteNonQuery();
            stock.Id = command.LastInsertedId.ToString();
        }

        /// <summary>
        /// 新增多筆資料進 StockPrice 資料表
        /// </summary>
        /// <param name="stocksList"></param>
        public void InsertBatch(List<StockPrice> stocksList)
        {
            StringBuilder sw = new StringBuilder();
            int count = stocksList.Count;
            sw.Append("INSERT INTO StockPrice ");
            sw.Append(
                "(Name,Code,Date,OpenPrice,ClosePrice,HeightPrice,LowerPrice,Volumn,TradeAmount,TradeShare,PriceSpread) VALUES");
            for (int i = 0; i < count; i++)
            {
                sw.Append("(");
                sw.Append("?Name" + i + ", ");
                sw.Append("?Code" + i + ", ");
                sw.Append("?Date" + i + ", ");
                sw.Append("?OpenPrice" + i + ", ");
                sw.Append("?ClosePrice" + i + ", ");
                sw.Append("?HeightPrice" + i + ", ");
                sw.Append("?LowerPrice" + i + ", ");
                sw.Append("?Volumn" + i + ", ");
                sw.Append("?TradeAmount" + i + ", ");
                sw.Append("?TradeShare" + i + ", ");
                sw.Append("?PriceSpread" + i );
                sw.Append(")");

                if (i != count - 1)
                    sw.Append(",");
            }
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sw.ToString();
            for (int i = 0; i < count; i++)
            {
                command.Parameters.AddWithValue("?Name" + i, stocksList[i].Name);
                command.Parameters.AddWithValue("?Code" + i, stocksList[i].Code);
                command.Parameters.AddWithValue("?Date" + i, stocksList[i].Date);
                command.Parameters.AddWithValue("?OpenPrice" + i, stocksList[i].OpenPrice);
                command.Parameters.AddWithValue("?ClosePrice" + i, stocksList[i].ClosePrice);
                command.Parameters.AddWithValue("?HeightPrice" + i, stocksList[i].HeightPrice);
                command.Parameters.AddWithValue("?LowerPrice" + i, stocksList[i].LowerPrice);
                command.Parameters.AddWithValue("?Volumn" + i, stocksList[i].Volumn);
                command.Parameters.AddWithValue("?TradeAmount" + i, stocksList[i].TradeAmount);
                command.Parameters.AddWithValue("?TradeShare" + i, stocksList[i].TradeShare);
                command.Parameters.AddWithValue("?PriceSpread" + i, stocksList[i].PriceSpread);
            }
            command.ExecuteNonQuery();
            for (int i = 0; i < count; i++)
                stocksList[i].Id = i == 0 ? command.LastInsertedId.ToString() : (command.LastInsertedId + i).ToString();
        }

        /// <summary>
        /// 刪除一筆資料進 StockPrice 資料表
        /// </summary>
        /// <param name="stock"></param>
        public void Delete(StockPrice stock)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM  StockPrice ";
            command.CommandText += " WHERE Code = ?Code AND OpenPrice = ?OpenPrice";
            command.Parameters.AddWithValue("?Code", stock.Code);
            command.Parameters.AddWithValue("?OpenPrice", stock.OpenPrice);
            command.ExecuteNonQuery();
            stock.Id = command.LastInsertedId.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<StockPrice> SelectByCode(string code)
        {
            List<StockPrice> result = new List<StockPrice>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM  StockPrice ";
            command.CommandText += "WHERE Code = ?Code Order By Date";
            command.Parameters.AddWithValue("?Code", code);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                StockPrice stock = new StockPrice();
                ReadAll(stock,reader);
                result.Add(stock);
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 測試時使用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public StockPrice Select(string id)
        {
            StockPrice stock = new StockPrice();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM StockPrice WHERE Id=?id";
            command.Parameters.AddWithValue("?id", id);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadAll(stock, reader);
            }
            reader.Close();
            return stock;
        }

        public void ReadAll(StockPrice stock, MySqlDataReader reader)
        {
            stock.Name = reader["Name"].ToString();
            stock.Code = reader["Code"].ToString();
            stock.Date = DateTime.Parse(reader["Date"].ToString()).ToString("yyyy-MM-dd"); 
            stock.OpenPrice = float.Parse(reader["OpenPrice"].ToString());
            stock.HeightPrice = float.Parse(reader["HeightPrice"].ToString());
            stock.LowerPrice = float.Parse(reader["LowerPrice"].ToString());
            stock.ClosePrice = float.Parse(reader["ClosePrice"].ToString());
            stock.TradeAmount = Int32.Parse(reader["TradeAmount"].ToString());
            stock.TradeShare = Int32.Parse(reader["TradeShare"].ToString());
            stock.PriceSpread = float.Parse(reader["PriceSpread"].ToString());
            stock.Volumn = Int32.Parse(reader["Volumn"].ToString());
        }
    }
}
