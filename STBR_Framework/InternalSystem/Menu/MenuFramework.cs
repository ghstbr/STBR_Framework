using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Utils;
using ME = STBR_Framework.Utils.ST_Menus;
using RE = STBR_Framework.InternalSystem.Forms.FormsFiles;

namespace STBR_Framework.InternalSystem.Menu
{
    [ST_Menu("mnuEventsView", "menu eventos")]
    [ST_Menu("mnuVorazTables", "")]
    [ST_Menu("mnuInfoAddon", "")]
    [ST_Menu("mnuVorazBaseDados", "")]
    internal class MenuFramework : ST_MenuBase
    {
        private string _icone = string.Empty;

        public MenuFramework()
        {

#if DEBUG
            ME.Add("4864", "Ferramentas Stbr", "mnuStbrFerramentas", 99, SAPbouiCOM.BoMenuType.mt_POPUP, _icone);

            ME.Add("mnuStbrFerramentas", "Infos Addon", "mnuInfoAddon", 99, SAPbouiCOM.BoMenuType.mt_STRING);

            ME.Add("mnuStbrFerramentas", "Geradores Layouts", "mnuStbrLayouts", 99, SAPbouiCOM.BoMenuType.mt_POPUP);
            ME.Add("mnuStbrLayouts", "Gerar Tabelas de usuários", "mnuStbrTables", 99, SAPbouiCOM.BoMenuType.mt_STRING);

            ME.Add("mnuStbrFerramentas", "Visualizar Eventos - Desligado", "mnuEventsView", 99, SAPbouiCOM.BoMenuType.mt_STRING);


#endif

            ME.Add("4864", "Stbr Gerenciar Addon", "mnuStbrGerenciarAddon", 99, SAPbouiCOM.BoMenuType.mt_POPUP, _icone);
            ME.Add("mnuStbrGerenciarAddon", "Base de Dados", "mnuStbrBaseDados", 99, SAPbouiCOM.BoMenuType.mt_STRING);

        }


        public override void MenuClick_After(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            switch (pVal.MenuUID)
            {
                case "mnuVorazTables":
                    ST_Forms.OpenUserForm("frmVorazTables", RE.frmTableFields);
                    break;
                case "mnuEventsView":
                    if (ST_B1AppDomain.Application.Menus.Item("mnuEventsView").String == "Visualizar Eventos - Desligado")
                    {
                        ST_B1AppDomain.Application.Menus.Item("mnuEventsView").String = "Visualizar Eventos - Ligado";
                        ST_B1AppDomain.EventsView = true;
                    }
                    else
                    {
                        ST_B1AppDomain.Application.Menus.Item("mnuEventsView").String = "Visualizar Eventos - Desligado";
                        ST_B1AppDomain.EventsView = false;
                    }

                    break;

                case "mnuInfoAddon":
                    ST_Forms.OpenUserForm("frmInfo", RE.frmInfo);
                    break;

                case "mnuVorazBaseDados":
                    ST_Forms.OpenUserForm("frmBaseDados", RE.frmBaseDados);
                    break;
            }
        }

    }
}
