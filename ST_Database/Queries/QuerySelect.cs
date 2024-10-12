using SAPbobsCOM;
using STBR_Framework;
using System;

namespace ST_Database.Queries
{
    internal class QuerySelect
    {
        public static string Select(string nameQuery)
        {
            try
            {
                string ret = "";
                switch (ST_B1AppDomain.Company.DbServerType)
                {
                    case BoDataServerTypes.dst_HANADB:
                        ret = hana.ResourceManager.GetString(nameQuery);
                        break;

                    default:
                        ret = sql.ResourceManager.GetString(nameQuery);
                        break;

                }
                return ret;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Error Select:", ex);
                return "";
            }
        }
    }
}
