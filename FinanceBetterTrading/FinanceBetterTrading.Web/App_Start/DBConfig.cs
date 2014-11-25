using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FinanceBetterTrading.Service;


namespace FinanceBetterTrading.Web.App_Start
{
    public static class DBConfig
    {
        public static void Register()
        {
            var tst = ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;
            DBConn.SetConnString(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        }
    }
}