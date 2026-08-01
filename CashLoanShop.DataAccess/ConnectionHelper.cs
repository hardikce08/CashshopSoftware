using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.EntityClient;
namespace CashLoanShop.DataAccess
{
    public class ConnectionHelper
    {
        public string DbConnectionStringKey
        {
            get
            {
                return ConfigurationManager.AppSettings["DbConnectionStringKey"];
                // return "";
            }
        }

        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings[DbConnectionStringKey].ConnectionString;
                //return ConfigurationManager.ConnectionStrings[DbConnectionStringKey].ConnectionString;
                //return "";

            }
        }

        static string _entityConnection = null;

        public string EntityConnectionString
        {
            get
            {

                if (string.IsNullOrEmpty(_entityConnection))
                {
                    EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
                    entityBuilder.Provider = "System.Data.SqlClient";
                    entityBuilder.ProviderConnectionString = ConnectionString;
                    entityBuilder.Metadata = @"res://*/CashLoanShopEntities.csdl|res://*/CashLoanShopEntities.ssdl|res://*/CashLoanShopEntities.msl";
                    _entityConnection = entityBuilder.ToString();
                }

                return _entityConnection;
            }
        }
    }
}
