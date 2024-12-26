using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.SAP.Database;
using STBR_Framework.Utils;

namespace STBR_Framework.Default.forms
{
    [ST_Form("frmBaseDados", "Template")]
    public class frmBaseDados : ST_FormBase
    {
        private Button btnAtualizarTabelas;
        private EditText etLog;
        private UserDataSource log;
        public override void OnCustomInitialize()
        {
            btnAtualizarTabelas = (Button)oForm.Items.Item("btAtualBd").Specific;
            etLog = (EditText)oForm.Items.Item("etLog").Specific;
            log = oForm.DataSources.UserDataSources.Item("UD_log");

        }

        public override void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            try
            {
                if (pVal.ItemUID == btnAtualizarTabelas.Item.UniqueID)
                {
                    new DB(ref log);
                    log.Value += "\r\n ======== ATUALIZAÇÃO COMPLETA! ========";
                }

            }
            catch (System.Exception ex)
            {
                ST_B1Exception.throwException("Erro ao pressionar botão :: ", ex);
                ST_Mensagens.StatusBarError(ex.Message);
            }
        }


    }
}
