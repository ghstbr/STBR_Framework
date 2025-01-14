using SAPbobsCOM;
using STBR_Framework.Queries;
using STBR_Framework.Utils;
using System;

namespace STBR_Framework.SAP.Utils
{
    public class ST_RegProducts
    {
        public void SetProducts(STBR_Framework.InternalSystem.Models.ST_Products model)
        {
            try
            {
                Recordset recSet = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                Recordset recProdExist = (Recordset)ST_B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                try
                {
                    string queryBusca = QuerySelect.Select("getProduct");
                    queryBusca = queryBusca.Replace("#Code", model.Code);
                    recProdExist.DoQuery(queryBusca);
                    string query = "";
                    if (!recProdExist.EoF)
                    {
                        query = QuerySelect.Select("updateProducts");
                    }
                    else
                    {
                        query = QuerySelect.Select("insertProducts");
                    }
                    query = query.Replace("#Code", model.Code);
                    query = query.Replace("#Name", model.Name);
                    query = query.Replace("#Description", model.Description);
                    query = query.Replace("#Key", model.Key);
                    query = query.Replace("#Active", model.Active);
                    query = query.Replace("#Version", model.Version);
                    query = query.Replace("#Msg", model.Msg);
                    recSet.DoQuery(query);
                    recSet.ST_ClearMemory();
                    recProdExist.ST_ClearMemory();
                }
                catch (Exception ex)
                {
                    recSet.ST_ClearMemory();
                    recProdExist.ST_ClearMemory();
                    ST_B1Exception.throwException("SetProducts", ex);
                    ST_Mensagens.StatusBarError("Erro ao tentar salvar os produtos");
                }

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("SetProducts", ex);
                ST_Mensagens.StatusBarError("Erro ao tentar salvar os produtos");
            }

        }
    }
}
