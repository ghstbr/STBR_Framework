using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STBR_Framework.Connections
{
    internal class ConnectODBC
    {
        private OdbcConnection Connect()
        {
            try
            {

                //Porta 3XX15 onde XX e o numero da instancia instalada
                string input = ST_B1AppDomain.Company.Server.ToString(); 
                string pattern = @"@(.*?):"; 
                Match match = Regex.Match(input, pattern);
                string _strServerName = match.Groups[1].Value + ":" + ST_B1AppDomain.Port;
                string _strLoginName = ST_B1AppDomain.UserDB;
                string _strPassword = ST_B1AppDomain.PasswdDB;
                string _strDatabaseName = ST_B1AppDomain.Company.CompanyDB;
                string strConnectionString = string.Empty;

                //nao precisa criar conexao odbc no windows
                if (IntPtr.Size == 8)
                {
                    //para conexao 64 bits
                    strConnectionString = string.Concat(strConnectionString, "Driver={HDBODBC};");
                }
                else
                {
                    //para conexao 32 bits
                    strConnectionString = string.Concat(strConnectionString, "Driver={HDBODBC32};");
                }

                strConnectionString = string.Concat(strConnectionString, "ServerNode=", _strServerName, ";");
                strConnectionString = string.Concat(strConnectionString, "UID=", _strLoginName, ";");
                strConnectionString = string.Concat(strConnectionString, "PWD=", _strPassword, ";");

                //strConnectionString = string.Concat(strConnectionString, "CS=", _strDatabaseName, ";");

                return new OdbcConnection(strConnectionString.ToString());



            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return null;
            }
        }

        internal string GetQuery(string query)
        {
            try
            {
                OdbcConnection conn = Connect();
                conn.Open();
                OdbcCommand cmd = new OdbcCommand(query, conn);
                OdbcDataReader reader = cmd.ExecuteReader();
                string result = string.Empty;
                while (reader.Read())
                {
                    result = reader.GetString(0);
                }
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return null;
            }
        }

        internal int ExecuteQuery(string query)
        {
            try
            {
                OdbcConnection conn = Connect();
                conn.Open();
                OdbcCommand cmd = new OdbcCommand(query, conn);
                int result = cmd.ExecuteNonQuery();
                conn.Close();
                return result;
            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return -1;
            }
        }

    }
}
