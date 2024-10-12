using SAPbouiCOM;
using System;

namespace STBR_Framework.Connections
{
    public class ConnectUI
    {

        public void Connect(Type[] types = null)
        {
            SAPbouiCOM.SboGuiApi objGUIApi = null;
            SAPbouiCOM.Application objApplication = null;
            SAPbobsCOM.Company objCompany = null;
            ST_B1AppDomain.ConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
            //"0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";

            try
            {
                objGUIApi = new SAPbouiCOM.SboGuiApi();
                objGUIApi.Connect(ST_B1AppDomain.ConnectionString);
                objApplication = objGUIApi.GetApplication(-1);
                objCompany = (SAPbobsCOM.Company)objApplication.Company.GetDICompany();

                if (objCompany.Connected)
                {
                    ST_B1AppDomain.Application = objApplication;
                    ST_B1AppDomain.Company = objCompany;
                    ST_B1AppDomain.Connected = true;
                    new Events();

                    if (types != null)
                        ST_B1AppDomain.CreateInstanceClass(types);






                    ST_B1AppDomain.Application.SetStatusBarMessage("Conexão estabelecida com sucesso!", BoMessageTime.bmt_Short, false);


                }
                else
                {
                    ST_B1AppDomain.Connected = false;
                }

            }
            catch (Exception er)
            {
                ST_B1AppDomain.Connected = false;
                ST_B1Exception.throwException("Erro ao conectar no SAP B1: ", er);

            }


        }



    }
}
