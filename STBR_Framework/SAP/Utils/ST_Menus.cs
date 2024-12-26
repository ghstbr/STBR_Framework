using SAPbouiCOM;
using System;

namespace STBR_Framework.Utils
{
    /// <summary>
    /// Classe para criacao de menus
    /// </summary>
    public static class ST_Menus
    {
        /// <summary>
        /// Adiciona um item de menu em um menu ja existente ou adicionado.
        /// </summary>
        /// <param name="mnu"></param>
        /// <param name="mnuItemB1ID">Id do menu pai onde o item de menu devera ser inserido</param>
        /// <param name="mnuItemDescr">Texto do item de menu</param>
        /// <param name="mnuItemID">ID do item de menu que esta sendo criado</param>
        /// <param name="position">posicao na lista de itens dentro de um menu</param>
        /// <param name="type">tipo...</param>
        /// <param name="imagePath">caminho para o arquivo de imagem a ser utilizado como icone no menu</param>
        /// <param name="remove"></param>
        internal static void Add(
            string mnuItemB1ID,
            string mnuItemDescr,
            string mnuItemID,
            int position,
            BoMenuType type,
            string imagePath = "",
            bool remove = true)
        {
            Menus oMenus = null;
            MenuItem oMenuItem = null;
            MenuCreationParams oCreationPackage = null;
            oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(ST_B1AppDomain.Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

            oMenuItem = ST_B1AppDomain.Application.Menus.Item(mnuItemB1ID);
            oMenus = oMenuItem.SubMenus;

            bool exist = (oMenus != null) && oMenuItem.SubMenus.Exists(mnuItemID);

            if (exist && remove)
            {
                oMenuItem.SubMenus.RemoveEx(mnuItemID);
                exist = false;
            }
            else
            {
                exist = false;
            }


            if (!(exist && remove))
            {
                oCreationPackage.Type = type;
                oCreationPackage.UniqueID = mnuItemID;
                oCreationPackage.String = mnuItemDescr;
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = position; //posição onde vai criar o modulo
                oCreationPackage.Image = imagePath;

                try
                {
                    if (oMenus == null)
                    {
                        oMenuItem.SubMenus.Add(mnuItemID, mnuItemDescr, type, position);
                        oMenus = oMenuItem.SubMenus;
                    }

                    oMenus.AddEx(oCreationPackage);
                }
                catch (Exception ex)
                {
                    ST_B1Exception.throwException("Erro ao criar Menu ::", ex);
                }
            }

        }


        internal static void AddContext(string uniqueId, string description, int position)
        {
            SAPbouiCOM.MenuItem oMenuItem = null;
            SAPbouiCOM.Menus oMenus = null;


            try
            {
                SAPbouiCOM.MenuCreationParams oCreationPackage = null;

                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(ST_B1AppDomain.Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

                oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                oCreationPackage.UniqueID = uniqueId;
                oCreationPackage.String = description;
                oCreationPackage.Enabled = true;
                oCreationPackage.Position = position;

                oMenuItem = ST_B1AppDomain.Application.Menus.Item("1280"); // Data'
                oMenus = oMenuItem.SubMenus;
                oMenus.AddEx(oCreationPackage);

            }
            catch
            {

            }
        }

        internal static void RemoveContext(string uniqueId)
        {
            try
            {
                ST_B1AppDomain.Application.Menus.RemoveEx(uniqueId);
            }
            catch
            {

            }
        }

        public static string GetMenuID(string name)
        {
            InfosAddon infosAddon = new InfosAddon();
            string menuId = string.Empty;
            menuId = "mnu" + infosAddon.CustomInfo.Prefix_name + name;
            return menuId;
        }

    }
}
