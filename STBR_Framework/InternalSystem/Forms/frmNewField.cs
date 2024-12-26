using STBR_Framework.Attributes;
using STBR_Framework.InternalSystem.Services;



namespace STBR_Framework.Default.forms
{
    [ST_Form("frmNewField", "Template")]
    public class frmNewField : ST_FormBase
    {
        private frmTableFields_service _service = new frmTableFields_service();
        public override void OnCustomInitialize()
        {

        }

        public override void ItemPressed_After(string formUID, ref SAPbouiCOM.ItemEvent pVal, ref bool bubbleEvent)
        {
            if (pVal.ItemUID == "1")
            {
                _service.NovoCampo(this.oForm, 
                    oForm.DataSources.UserDataSources.Item("UD_NomeC").ValueEx,
                    oForm.DataSources.UserDataSources.Item("UD_NomeT").ValueEx);
            }
        }
    }
}
