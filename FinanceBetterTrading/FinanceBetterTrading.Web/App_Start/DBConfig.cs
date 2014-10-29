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
            DBConn.SetConnString(ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString);
        }
    }
}