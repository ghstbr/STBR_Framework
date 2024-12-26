using STBR_Framework.Models;
using STBR_Framework.SAP.Services;

namespace STBR_Framework.InternalSystem.Services
{
    internal class frmBaseDados_service
    {
        private InfosAddon_service _infos = new InfosAddon_service();
        private InfoModel _info = new InfoModel();

        public frmBaseDados_service()
        {
            _info = _infos.ReadInfoJson();
        }

        internal void AtualizaBd(ref string log)
        {

        }



    }
}
