using System.Xml;
using System;
using SAPbouiCOM;
using STBR_Framework;

namespace ST_Extensions
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Abre tela no SAP com base no XML '.srf' passado como parametro
        /// </summary>
        /// <param name="app"></param>
        /// <param name="screen">Tela XML</param>
        /// <param name="screenName">ID da tela XML</param>
        /// <returns></returns>
        public static Form ST_OpenForm(this Application app, string screen, string screenName)
        {

            try
            {
                FormCreationParams creationPackage = (FormCreationParams)ST_B1AppDomain.Application.CreateObject(BoCreatableObjectType.cot_FormCreationParams);
                XmlDocument document = new XmlDocument
                {
                    InnerXml = screen
                };
                creationPackage.XmlData = document.InnerXml;
                creationPackage.UniqueID = DateTime.Now.ToBinary().ToString();
                creationPackage.FormType = screenName;
                Form frm = ST_B1AppDomain.Application.Forms.AddEx(creationPackage);

                return frm;

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao carregar formularios XML :: ", ex);
                return null;
            }




        }


    }
}
