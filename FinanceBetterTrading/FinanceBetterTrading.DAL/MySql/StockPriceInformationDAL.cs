using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using MySql.Data.MySqlClient;

namespace FinanceBetterTrading.DAL
{
    public class StockPriceInformationDal:MySqlDal
    {
        /// <summary>
        /// 新增一筆資料進 StockPriceInformation 資料表
        /// </summary>
        /// <param name="stock"></param>
        public void Insert(StockPriceInformation stock)
        {
            
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO StockPriceInformation ";
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
        /// 刪除一筆資料進 StockPriceInformation 資料表
        /// </summary>
        /// <param name="stock"></param>
        public void Delete(StockPriceInformation stock)
        {

            MySqlCommand command = connection.CreateCommand();
            //command.CommandText = "INSERT INTO StockPriceInformation ";
            //command.CommandText +=
            //    "(Name,Code,Date,OpenPrice,ClosePrice,HeightPrice,LowerPrice,Volumn,TradeAmount,TradeShare,PriceSpread) ";
            //command.CommandText += "VALUES (?Name, ?Code, ?Date, ?OpenPrice, ?ClosePrice, ?HeightPrice, ?LowerPrice, ?Volumn, ?TradeAmount, ?TradeShare,?PriceSpread)";
            //command.Parameters.AddWithValue("?Name", stock.Name);
            //command.Parameters.AddWithValue("?Code", stock.Code);
            //command.Parameters.AddWithValue("?Date", stock.Date);
            //command.Parameters.AddWithValue("?OpenPrice", stock.OpenPrice);
            //command.Parameters.AddWithValue("?ClosePrice", stock.ClosePrice);
            //command.Parameters.AddWithValue("?HeightPrice", stock.HeightPrice);
            //command.Parameters.AddWithValue("?LowerPrice", stock.LowerPrice);
            //command.Parameters.AddWithValue("?Volumn", stock.Volumn);
            //command.Parameters.AddWithValue("?TradeAmount", stock.TradeAmount);
            //command.Parameters.AddWithValue("?TradeShare", stock.TradeShare);
            //command.Parameters.AddWithValue("?PriceSpread", stock.PriceSpread);
            command.CommandText = "DELETE FROM  StockPriceInformation WHERE ";
            command.CommandText += "Code = ?Code AND OpenPrice = ?OpenPrice";
            command.Parameters.AddWithValue("?Code", stock.Code);
            command.Parameters.AddWithValue("?OpenPrice", stock.OpenPrice);
            command.ExecuteNonQuery();
            stock.Id = command.LastInsertedId.ToString();
        }
        /// <summary>
        /// 新增多筆資料進 StockPriceInformation 資料表
        /// </summary>
        /// <param name="stocks"></param>
        public void Insert(List<StockPriceInformation> stocks)
        {
            
        }

        public StockPriceInformation Select(string id)
        {
            StockPriceInformation stock = new StockPriceInformation();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM StockPriceInformation WHERE Id=?id";
            command.Parameters.AddWithValue("?id", id);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadAll(stock, reader);
            }
            reader.Close();
            return stock;
        }

        public void ReadAll(StockPriceInformation stock, MySqlDataReader reader)
        {
            stock.Name = reader["Name"].ToString();
            stock.Code = reader["Code"].ToString();
            stock.Date = reader["Date"].ToString();
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
