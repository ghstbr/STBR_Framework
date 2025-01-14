using STBR_Framework.Default.forms;
using STBR_Framework.InternalSystem.forms;
using STBR_Framework.InternalSystem.Models;

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
            new ST_CF();
            new ST_Products();
        }
    }
}
