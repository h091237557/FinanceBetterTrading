using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceBetterTrading.Domain;
using MySql.Data.MySqlClient;

namespace FinanceBetterTrading.DAL
{
    public class InstitutionalInvestorsInformationDAL:MySqlDal
    {
        /// <summary>
        /// 新增多筆資料進 StockPriceInformation 資料表
        /// </summary>
        /// <param name="InstitutionalInvestorsList"></param>
        public void InsertBatch(List<InstitutionalInvestorsScheduleDataInformation> InstitutionalInvestorsList)
        {
            StringBuilder sw = new StringBuilder();
            int count = InstitutionalInvestorsList.Count;
            sw.Append("INSERT INTO institutionalinvestorsscheduledatainformation ");
            sw.Append(
                "(Date,StockCode,ForeignCapitalBuyShares,ForeignCapitalSellShares,InvestmentTrustBuyShares,InvestmentTrustSellShares,DealerBuySharesProprietaryTrading,DealerSellSharesProprietaryTrading,DealerBuySharesHedge,DealerSellSharesHedge,InstitutionalInvestorsBuySellShares) VALUES");
            for (int i = 0; i < count; i++)
            {
                sw.Append("(");
                sw.Append("?Date" + i + ", ");
                sw.Append("?StockCode" + i + ", ");
                sw.Append("?ForeignCapitalBuyShares" + i + ", ");
                sw.Append("?ForeignCapitalSellShares" + i + ", ");
                sw.Append("?InvestmentTrustBuyShares" + i + ", ");
                sw.Append("?InvestmentTrustSellShares" + i + ", ");
                sw.Append("?DealerBuySharesProprietaryTrading" + i + ", ");
                sw.Append("?DealerSellSharesProprietaryTrading" + i + ", ");
                sw.Append("?DealerBuySharesHedge" + i + ", ");
                sw.Append("?DealerSellSharesHedge" + i + ", ");
                sw.Append("?InstitutionalInvestorsBuySellShares" + i);
                sw.Append(")");

                if (i != count - 1)
                    sw.Append(",");
            }
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = sw.ToString();
            for (int i = 0; i < count; i++)
            {
                command.Parameters.AddWithValue("?Date" + i, InstitutionalInvestorsList[i].Date);
                command.Parameters.AddWithValue("?StockCode" + i, InstitutionalInvestorsList[i].StockCode);
                command.Parameters.AddWithValue("?ForeignCapitalBuyShares" + i, InstitutionalInvestorsList[i].ForeignCapitalBuyShares);
                command.Parameters.AddWithValue("?ForeignCapitalSellShares" + i, InstitutionalInvestorsList[i].ForeignCapitalSellShares);
                command.Parameters.AddWithValue("?InvestmentTrustBuyShares" + i, InstitutionalInvestorsList[i].InvestmentTrustBuyShares);
                command.Parameters.AddWithValue("?InvestmentTrustSellShares" + i, InstitutionalInvestorsList[i].InvestmentTrustSellShares);
                command.Parameters.AddWithValue("?DealerBuySharesProprietaryTrading" + i, InstitutionalInvestorsList[i].DealerBuySharesProprietaryTrading);
                command.Parameters.AddWithValue("?DealerSellSharesProprietaryTrading" + i, InstitutionalInvestorsList[i].DealerSellSharesProprietaryTrading);
                command.Parameters.AddWithValue("?DealerBuySharesHedge" + i, InstitutionalInvestorsList[i].DealerBuySharesHedge);
                command.Parameters.AddWithValue("?DealerSellSharesHedge" + i, InstitutionalInvestorsList[i].DealerSellSharesHedge);
                command.Parameters.AddWithValue("?InstitutionalInvestorsBuySellShares" + i, InstitutionalInvestorsList[i].InstitutionalInvestorsBuySellShares);
            }
            command.ExecuteNonQuery();
            //for (int i = 0; i < count; i++)
            //    InstitutionalInvestorsList[i].Id = i == 0 ? command.LastInsertedId.ToString() : (command.LastInsertedId + i).ToString();
        }
        public InstitutionalInvestorsScheduleDataInformation Select(string date)
        {
            InstitutionalInvestorsScheduleDataInformation InstitutionalInvestors = new InstitutionalInvestorsScheduleDataInformation();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM institutionalinvestorsscheduledatainformation WHERE Date=?Date";
            command.Parameters.AddWithValue("?Date", date);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                ReadAll(InstitutionalInvestors, reader);
            }
            reader.Close();
            return InstitutionalInvestors;
        }
        public void ReadAll(InstitutionalInvestorsScheduleDataInformation InstitutionalInvestors, MySqlDataReader reader)
        {
            InstitutionalInvestors.Date = reader["Date"].ToString();
            InstitutionalInvestors.StockCode = reader["StockCode"].ToString();
            InstitutionalInvestors.ForeignCapitalBuyShares = Int32.Parse(reader["ForeignCapitalBuyShares"].ToString());
            InstitutionalInvestors.ForeignCapitalSellShares = Int32.Parse(reader["ForeignCapitalSellShares"].ToString());
            InstitutionalInvestors.InvestmentTrustBuyShares = Int32.Parse(reader["InvestmentTrustBuyShares"].ToString());
            InstitutionalInvestors.InvestmentTrustSellShares = Int32.Parse(reader["InvestmentTrustSellShares"].ToString());
            InstitutionalInvestors.DealerBuySharesProprietaryTrading = Int32.Parse(reader["DealerBuySharesProprietaryTrading"].ToString());
            InstitutionalInvestors.DealerSellSharesProprietaryTrading = Int32.Parse(reader["DealerSellSharesProprietaryTrading"].ToString());
            InstitutionalInvestors.DealerBuySharesHedge = Int32.Parse(reader["DealerBuySharesHedge"].ToString());
            InstitutionalInvestors.DealerSellSharesHedge = Int32.Parse(reader["DealerSellSharesHedge"].ToString());
            InstitutionalInvestors.InstitutionalInvestorsBuySellShares = Int32.Parse(reader["InstitutionalInvestorsBuySellShares"].ToString());
        }
    }
}
