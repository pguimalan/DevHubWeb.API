using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DevHubWeb.Data.DataHelpers
{
    public abstract class DataManager
    {
        protected IDbConnection GetDbConnection(string dbConnection)
        {
            return new SqlConnection(dbConnection);
        }
    }
}
