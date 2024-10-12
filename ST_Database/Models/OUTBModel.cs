using SAPbobsCOM;
using ST_Database.Queries;
using ST_Extensions;
using STBR_Framework;
using System;
using System.Collections.Generic;

namespace ST_Database.Models
{
    internal class OUTBModel
    {
        public string TableName { get; set; }
        public string Descr { get; set; }
        public int TblNum { get; set; }
        public string ObjectType { get; set; }
        public string UsedInObj { get; set; }
        public string LogTable { get; set; }
        public string Archivable { get; set; }
        public string ArchivDate { get; set; }


        internal List<OUTBModel> Tables()
        {
            try
            {
                List<OUTBModel> tbs = new List<OUTBModel>();

                Recordset oRs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                tbs = oRs.ST_DoQuery(QuerySelect.Select("outbList"), this);

                oRs.ST_ClearMemory();
                return tbs;

            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
                return null;
            }
        }



    }
}

