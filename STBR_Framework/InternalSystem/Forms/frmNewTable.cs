using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.InternalSystem.Services;

namespace STBR_Framework.Default.forms
{
    [ST_Form("frmNewTable", "Nova Tabela")]
    public class frmNewTable : ST_FormBase
    {
        frmTableFields_service service = new frmTableFields_service();

        public override void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if(pVal.ItemUID == "1")
            {                
                service.NovaTabela(this.oForm);
            }
        }


    }
}
