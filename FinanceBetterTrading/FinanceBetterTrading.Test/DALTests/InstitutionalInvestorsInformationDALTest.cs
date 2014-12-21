using System;
using System.Collections.Generic;
using FinanceBetterTrading.Web.App_Start;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceBetterTrading.DAL;
using FinanceBetterTrading.Domain;
using FinanceBetterTrading.Service;
using System.Data;
using System.Transactions;


namespace FinanceBetterTrading.Test.DALTests
{
    [TestClass]
    public class InstitutionalInvestorsInformationDALTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            DBConfig.Register();
        }
        /// <summary>
        /// 測試Insert
        /// </summary>
        [TestMethod]
        public void TestInsertBatch()
        {
            InstitutionalInvestorsInformationDAL institutionalinvestorsInformationDAL = new InstitutionalInvestorsInformationDAL();
            try
            {
               // using (var scrop = new TransactionScope())
                //{
                    //institutionalinvestorsInformationDAL.Open(DBConn.Conn);
                    //var InstitutionalInvestorsList = CreateInstitutionalInvestorsObject();
                    //institutionalinvestorsInformationDAL.InsertBatch(InstitutionalInvestorsList);

                    //var actual = institutionalinvestorsInformationDAL.Select(InstitutionalInvestorsList.Date).Name;
                    //var expect = InstitutionalInvestorsList.StockCode;

                    //Assert.AreEqual(expect, actual);
                    institutionalinvestorsInformationDAL.Open(DBConn.Conn);
                    List<InstitutionalInvestorsScheduleDataInformation> institutionalInvestorsScheduleDataInformations = new List<InstitutionalInvestorsScheduleDataInformation>();
                    institutionalInvestorsScheduleDataInformations.Add(CreateInstitutionalInvestorsObject());
                    institutionalInvestorsScheduleDataInformations.Add(CreateInstitutionalInvestorsObject());

                    institutionalinvestorsInformationDAL.InsertBatch(institutionalInvestorsScheduleDataInformations);

                    var actual = institutionalinvestorsInformationDAL.Select(institutionalInvestorsScheduleDataInformations[0].Date).StockCode;
                    var expect = institutionalInvestorsScheduleDataInformations[0].StockCode;

                    Assert.AreEqual(expect, actual);         
                    //scrop.Complete();
                //}
            }
            finally
            {
                if (institutionalinvestorsInformationDAL.Connection != null)
                    institutionalinvestorsInformationDAL.Connection.Close();
            }
        }
        private InstitutionalInvestorsScheduleDataInformation CreateInstitutionalInvestorsObject()
        {
            InstitutionalInvestorsScheduleDataInformation InstitutionalInvestorsList = new InstitutionalInvestorsScheduleDataInformation();
            InstitutionalInvestorsList.Date = "20141219";
            InstitutionalInvestorsList.StockCode = "1234";
            InstitutionalInvestorsList.ForeignCapitalBuyShares = 123456789;
            InstitutionalInvestorsList.ForeignCapitalSellShares = 123456789;
            InstitutionalInvestorsList.InvestmentTrustBuyShares = 123456789;
            InstitutionalInvestorsList.InvestmentTrustSellShares = 123456789;
            InstitutionalInvestorsList.DealerBuySharesProprietaryTrading = 123456789;
            InstitutionalInvestorsList.DealerSellSharesProprietaryTrading = 123456789;
            InstitutionalInvestorsList.DealerBuySharesHedge = 123456789;
            InstitutionalInvestorsList.DealerSellSharesHedge = 123456789;
            InstitutionalInvestorsList.InstitutionalInvestorsBuySellShares = 123456789;
            return InstitutionalInvestorsList;
        }
    }
}
