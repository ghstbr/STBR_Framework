using STBR_Framework.Models;
using STBR_Framework.SAP.Services;

namespace STBR_Framework
{
    public class InfosAddon
    {
        private InfosAddon_service InfosService;
        public InfoModel CustomInfo { get; set; }
        public InfosAddon()
        {
            InfosService = new InfosAddon_service();
            CustomInfo = new InfoModel();
            CustomInfo = InfosService.ReadInfoJson();
        }

    }
}
