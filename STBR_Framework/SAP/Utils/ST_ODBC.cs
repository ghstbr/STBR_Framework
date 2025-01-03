using STBR_Framework.Connections;
using System;

namespace STBR_Framework.Utils
{
    public static class ST_ODBC
    {
        public static int ExecuteNonQuery(string query)
        {
            try
            {
                ConnectODBC connectODBC = new ConnectODBC();
                return connectODBC.ExecuteQuery(query);
            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return -1;
            }
        }

    }
}
