using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;

namespace TodoServer
{
    public class connection
    {
        public   string ConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP - 3QTD5NT";
            builder.InitialCatalog = "todolist";
            builder.IntegratedSecurity = true;
            builder.TrustServerCertificate = true;
            return builder.ConnectionString;
        }
    }
}