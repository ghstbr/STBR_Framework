using SAPbouiCOM;
using STBR_Framework.Attributes;
using STBR_Framework.Utils;
using ME = STBR_Framework.Utils.ST_Menus;
using RE = STBR_Framework.InternalSystem.Forms.FormsFiles;

namespace STBR_Framework.InternalSystem.Menu
{
    [ST_MenuInternal("EventsView", "menu eventos")]
    [ST_MenuInternal("Tables", "")]
    [ST_MenuInternal("InfoAddon", "")]
    [ST_MenuInternal("BaseDados", "")]
    internal class MenuFramework : ST_MenuBaseInternal
    {
        private string _icone = string.Empty;

        public MenuFramework()
        {

#if DEBUG
            ME.Add("4864", "Ferramentas STBR", ST_Menus.GetMenuID("Ferramentas"), 99, SAPbouiCOM.BoMenuType.mt_POPUP, _icone);

            ME.Add(ST_Menus.GetMenuID("Ferramentas"), "Infos Addon", ST_Menus.GetMenuID("InfoAddon"), 99, SAPbouiCOM.BoMenuType.mt_STRING);

            ME.Add(ST_Menus.GetMenuID("Ferramentas"), "Geradores Layouts", ST_Menus.GetMenuID("Layouts"), 99, SAPbouiCOM.BoMenuType.mt_POPUP);
            ME.Add(ST_Menus.GetMenuID("Layouts"), "Gerar Tabelas de usuários", ST_Menus.GetMenuID("Tables"), 99, SAPbouiCOM.BoMenuType.mt_STRING);

            ME.Add(ST_Menus.GetMenuID("Ferramentas"), "Visualizar Eventos - Desligado", ST_Menus.GetMenuID("EventsView"), 99, SAPbouiCOM.BoMenuType.mt_STRING);


#endif

            ME.Add("43523", "Administrador Addons STBR", ST_Menus.GetMenuID("AdmAddonStbr"), 4, SAPbouiCOM.BoMenuType.mt_POPUP, _icone);
            ME.Add(ST_Menus.GetMenuID("AdmAddonStbr"), "Base de Dados", ST_Menus.GetMenuID("BaseDados"), 99, SAPbouiCOM.BoMenuType.mt_STRING);

        }


        public override void MenuClick_After(ref MenuEvent pVal, ref bool bubbleEvent)
        {
            string menuUID = pVal.MenuUID;
            if (menuUID == ST_Menus.GetMenuID("InfoAddon"))
                ST_Forms.OpenUserForm("frmInfo", RE.frmInfo);
            else if (menuUID == ST_Menus.GetMenuID("Tables"))
                ST_Forms.OpenUserForm("frmTableFields", RE.frmTableFields);
            else if (menuUID == ST_Menus.GetMenuID("EventsView"))
            {
                if (ST_B1AppDomain.Application.Menus.Item(ST_Menus.GetMenuID("EventsView")).String == "Visualizar Eventos - Desligado")
                {
                    ST_B1AppDomain.Application.Menus.Item(ST_Menus.GetMenuID("EventsView")).String = "Visualizar Eventos - Ligado";
                    ST_B1AppDomain.EventsView = true;
                }
                else
                {
                    ST_B1AppDomain.Application.Menus.Item(ST_Menus.GetMenuID("EventsView")).String = "Visualizar Eventos - Desligado";
                    ST_B1AppDomain.EventsView = false;
                }
            }
            else if (menuUID == ST_Menus.GetMenuID("BaseDados"))
                ST_Forms.OpenUserForm("frmBaseDados", RE.frmBaseDados);



        }

    }
}
