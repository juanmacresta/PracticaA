using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


namespace Tombolini.Datos
{
    public class Base
    {
        const String consKeyDefaultCnnString = "ConnStringLocal";
        private SqlConnection _xxxConnection;
        public SqlConnection xxxConnection  { get=>_xxxConnection; set=>_xxxConnection=value; }

        protected void OpenConnection()
        {
            String conn = ConfigurationManager.ConnectionStrings[consKeyDefaultCnnString].ConnectionString;
            xxxConnection = new SqlConnection(conn);
            xxxConnection.Open();
        }

        protected void CloseConnection() {
            xxxConnection.Close();
            xxxConnection = null;
        }


    }
}
