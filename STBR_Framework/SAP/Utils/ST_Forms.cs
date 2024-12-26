using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Xml;

namespace STBR_Framework.Utils
{
    public class ST_Forms
    {
        public static Form OpenUserForm(string idForm, string formulario, Dictionary<string, object> parms = null)
        {
            try
            {
                FormCreationParams creationPackage = ((FormCreationParams)(ST_B1AppDomain.Application.CreateObject(BoCreatableObjectType.cot_FormCreationParams)));
                XmlDocument oXmlDoc = new XmlDocument
                {
                    InnerXml = formulario
                };
                creationPackage.XmlData = oXmlDoc.InnerXml;
                creationPackage.UniqueID = DateTime.Now.ToBinary().ToString();
                creationPackage.FormType = idForm;
                Form oForm = ST_B1AppDomain.Application.Forms.AddEx(creationPackage);
                if (parms != null)
                    ST_B1AppDomain.DictionaryFormEvent[idForm].formParameters = parms;
                ST_B1AppDomain.DictionaryFormEvent[idForm].CapturaFormulario();
                ST_B1AppDomain.DictionaryFormEvent[idForm].OnCustomInitialize();
                


                return oForm;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao carregar folumario XML :: ", ex);
                return null;
            }

        }

        public static Form OpenUDOForm(string idUDO, string idKey)
        {
            try
            {
                return ST_B1AppDomain.Application.OpenForm(BoFormObjectEnum.fo_UserDefinedObject, idUDO, idKey);

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao abrir UDO :: ", ex);
                return null;
            }
        }

        public static Form OpenSystemForm(BoFormObjectEnum typeForm, string idKey)
        {
            try
            {
                return ST_B1AppDomain.Application.OpenForm(typeForm, "", idKey);
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao abrir Formulario de sistema :: ", ex);
                return null;
            }
        }

        public static void OpenWebBrowser(string url, Form oForm)
        {
            try
            {
                Item oItem = oForm.Items.Add("WebBrowser", BoFormItemTypes.it_WEB_BROWSER);
                oItem.Top = 0;
                oItem.Left = 0;
                oItem.Width = oForm.Width;
                oItem.Height = oForm.Height;
                WebBrowser oWebBrowser = (WebBrowser)oItem.Specific;
                oWebBrowser.Url = url;

            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao abrir WebBrowser :: ", ex);

            }
        }

        
    }
}
