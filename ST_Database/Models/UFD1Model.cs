using SAPbobsCOM;
using ST_Database.Queries;
using ST_Extensions;
using STBR_Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ST_Database.Models
{
    internal class UFD1Model
    {
        public string TableID { get; set; }
        public int FieldID { get; set; }
        public int IndexID { get; set; }
        public string FldValue { get; set; }
        public string Descr { get; set; }
        public DateTime FldDate { get; set; }


        internal List<UFD1Model> FieldItems(string tableName, string name)
        {
            try
            {
                List<UFD1Model> fieldItems = new List<UFD1Model>();

                Recordset oRs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                fieldItems = oRs.ST_DoQuery(String.Format(QuerySelect.Select("ufd1List"), tableName, name), this);
                oRs.ST_ClearMemory();
                return fieldItems;

            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return null;
            }
        }

    }
}
