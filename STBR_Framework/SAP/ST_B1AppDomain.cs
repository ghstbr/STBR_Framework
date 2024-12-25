using SAPbouiCOM.Framework;
using STBR_Framework.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace STBR_Framework
{
    /// <summary>
    /// Classe de dominio das conexoes estabelecidas
    /// </summary>
    public sealed class ST_B1AppDomain
    {
        //dicionarios de classes com atributos controlados
        internal static Dictionary<string, ST_FormBase> DictionaryFormEvent = new Dictionary<string, ST_FormBase>();
        internal static Dictionary<string, ST_MenuBase> DictionaryMenuEvent = new Dictionary<string, ST_MenuBase>();
        internal static Dictionary<string, string> DictionaryMenuDefault = new Dictionary<string, string>();
        

        static private ST_B1AppDomain objAppDomainClass = null;
        static private SAPbouiCOM.Application objApplication = null;
        static private SAPbobsCOM.Company objCompany = null;
        static private string strConnectionString = "";

        #region Publico

        static public bool Connected { get; set; }
        static public SAPbouiCOM.Application Application
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                return objApplication;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                objApplication = value;
                AppDomain.CurrentDomain.SetData("SAPApplication", objApplication);
            }
        }
        static public SAPbobsCOM.Company Company
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                return objCompany;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                objCompany = value;
                AppDomain.CurrentDomain.SetData("SAPCompany", objCompany);

            }
        }
        static public string ConnectionString
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                return strConnectionString;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new ST_B1AppDomain();
                strConnectionString = value;
                AppDomain.CurrentDomain.SetData("SAPConnectionString", strConnectionString);

            }
        }


        #endregion


        #region Interno

        internal static void RegisterMenuByType(string menuUid, ST_MenuBase menuBase)
        {
            if (!string.IsNullOrEmpty(menuUid))
            {
                DictionaryMenuEvent.Add(menuUid, menuBase);
            }
        }

        internal static void RegisterFormByType(string formUid, ST_FormBase formBase)
        {

            if (!string.IsNullOrEmpty(formUid))
            {
                DictionaryFormEvent.Add(formUid, formBase);
            }

        }

        internal static void CreateInstanceClass(Type[] nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            foreach (Type type in nameSpace)
            {
                if (type.Name != "Program" && type.CustomAttributes.Count() > 0)
                {
                    try
                    {
                        foreach (object obj2 in type.GetCustomAttributes(false))
                        {
                            if (obj2 is ST_MenuAttribute || obj2 is ST_FormAttribute)
                            {
                                Activator.CreateInstance(type);
                            }
                        }

                    }
                    catch { }

                }

            }

        }


        #endregion
    }
}
