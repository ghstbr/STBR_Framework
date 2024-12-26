using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Utils;

namespace STBR_Framework.InternalSystem.forms
{
    [ST_Form("frmUdo", "Template")]
    public class frmUdo : ST_FormBase
    {

        private DataTable dtUdo;
        internal string FileName;

        public override void OnCustomInitialize()
        {
            dtUdo = oForm.DataSources.DataTables.Item("DT_Udo");
            dtUdo.Rows.Add();
            
        }


    }
}
