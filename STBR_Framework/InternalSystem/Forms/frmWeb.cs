using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Utils;

namespace STBR_Framework.Default.forms
{
    [ST_Form("frmWeb", "Template")]
    public class frmWeb : ST_FormBase
    {
        public override void OnCustomInitialize()
        {
            ST_Forms.OpenWebBrowser("https://voraztecnologia.com/", oForm);
        }


        public override void Form_Resize_After(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            try
            {
                if (oForm != null)
                {
                    oForm.Items.Item("WebBrowser").Width = oForm.Width - 20;
                    oForm.Items.Item("WebBrowser").Height = oForm.Height - 20;
                }
            }
            catch (System.Exception ex)
            {
                ST_B1Exception.throwException("Erro ao redimensionar formulario :: ", ex);

            }
        }

    }
}
