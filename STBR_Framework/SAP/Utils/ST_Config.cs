using SAPbobsCOM;
using STBR_Framework.InternalSystem.Models;
using STBR_Framework.Queries;
using STBR_Framework.Utils;

namespace STBR_Framework.SAP.Utils
{
    public class ST_Config
    {

        public ST_CF GetConfig()
        {
            ST_CF config = new ST_CF();
            Recordset rs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {                
                rs.DoQuery(QuerySelect.Select("getConfig"));
                if (!rs.EoF)
                {
                    config.UserDB = rs.Fields.Item("U_UserDB").Value.ToString();
                    config.PasswdDB = rs.Fields.Item("U_PasswdDB").Value.ToString();
                    config.Port = rs.Fields.Item("U_Port").Value.ToString();
                    config.LicenseUrl = rs.Fields.Item("U_LicenseUrl").Value.ToString();
                    config.HardwareKey = rs.Fields.Item("U_HardwareKey").Value.ToString();
                    config.ClientId = rs.Fields.Item("U_ClientId").Value.ToString();


                }

                rs.ST_ClearMemory();                
                return config;

            }
            catch (System.Exception ex)
            {
                rs.ST_ClearMemory();
                ST_B1Exception.throwException("GetConfig", ex);
                return null;
            }
        }

        public void SetConfig(ST_CF config)
        {
            Recordset rs = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            Recordset rs2 = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            try
            {
                rs2.DoQuery(QuerySelect.Select("getConfig"));
                string query = "";  
                if (!rs2.EoF)
                {
                    query = QuerySelect.Select("updateConfig");
                }
                else
                {
                    query = QuerySelect.Select("insertConfig");
                }
                                
                query = query.Replace("#UserDB", config.UserDB);
                query = query.Replace("#PasswdDB", config.PasswdDB);
                query = query.Replace("#Port", config.Port);
                if(string.IsNullOrEmpty(config.LicenseUrl))
                {
                    config.LicenseUrl = "http://licencas.sap.tec.br";
                }
                query = query.Replace("#LicenseUrl", config.LicenseUrl);
                query = query.Replace("#HardwareKey", config.HardwareKey);
                query = query.Replace("#ClientId", config.ClientId);

                rs.DoQuery(query);
                rs.ST_ClearMemory();
                rs2.ST_ClearMemory();
                ST_Mensagens.StatusBarSuccess("Configuração salva com sucesso!");
            }
            catch (System.Exception ex)
            {
                rs.ST_ClearMemory();
                rs2.ST_ClearMemory();
                ST_B1Exception.throwException("SetConfig", ex);
            }
        }

        
    }
}
