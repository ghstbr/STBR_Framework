using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Models;
using STBR_Framework.SAP.Services;


namespace STBR_Framework.Default.forms
{
    [ST_Form("frmInfo", "Informações")]
    internal class frmInfo : ST_FormBase
    {

        private InfosAddon_service _service = new InfosAddon_service();

        public override void OnCustomInitialize()
        {
            InfoModel infos = _service.ReadInfoJson();
            DataTable oDT = oForm.DataSources.DataTables.Item("DT_Infos");
            oDT.Rows.Add();
            oDT.SetValue("DirectoryTables", 0, infos.DirectoryTables);
            oDT.SetValue("FilterFiles", 0, infos.FilterFiles);
            oDT.SetValue("Prefix_name", 0, infos.Prefix_name);
            oDT.SetValue("Prefix_description", 0, infos.Prefix_description);

        }

        public override void ItemPressed_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            if(pVal.ItemUID == "1" && pVal.ActionSuccess)
            {
                DataTable oDT = oForm.DataSources.DataTables.Item("DT_Infos");
                InfoModel infos = new InfoModel();
                infos.DirectoryTables = (string)oDT.GetValue("DirectoryTables", 0);
                infos.FilterFiles = (string)oDT.GetValue("FilterFiles", 0);
                infos.Prefix_name = (string)oDT.GetValue("Prefix_name", 0);
                infos.Prefix_description = (string)oDT.GetValue("Prefix_description", 0);
                _service.SaveInfoJson(infos);
            }
        }


    }
}
