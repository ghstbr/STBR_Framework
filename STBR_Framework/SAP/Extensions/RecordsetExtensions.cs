using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace STBR_Framework
{
    public static class RecordsetExtensions
    {

        public static List<TEntity> ST_DoQuery<TEntity>(this Recordset oRs, string query, TEntity table)
        {
            try
            {
                List<TEntity> list = new List<TEntity>();
                Recordset rs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                rs.DoQuery(query);
                while (!rs.EoF)
                {
                    TEntity t = (TEntity)Activator.CreateInstance(typeof(TEntity));
                    foreach (PropertyInfo info in t.GetType().GetProperties())
                    {
                        try
                        {
                            info.SetValue(t, rs.Fields.Item(info.Name).Value);
                        }
                        catch { }

                    }
                    list.Add(t);

                    rs.MoveNext();
                }
                rs.ST_ClearMemory();
                return list;
            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return null;
            }
        }

        public static void ST_DoQuery(this Recordset oRs, string query)
        {
            try
            {
                if (oRs == null)
                    oRs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRs.DoQuery(query);
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao executar query: " + query, ex);
            }
        }


    }
}
