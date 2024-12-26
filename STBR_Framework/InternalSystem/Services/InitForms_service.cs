using STBR_Framework.Default.forms;
using STBR_Framework.InternalSystem.forms;

namespace STBR_Framework.InternalSystem.Services
{
    internal class InitForms_service
    {
        internal static void Start()
        {
            new frmTableFields();
            new frmInfo();
            new frmNewTable();
            new frmNewField();
            new frmBaseDados();
            new frmWeb();
            new frmUdo();
        }
    }
}
