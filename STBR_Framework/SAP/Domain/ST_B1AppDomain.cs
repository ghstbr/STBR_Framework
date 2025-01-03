using SAPbouiCOM.Framework;
using STBR_Framework.Attributes;
using STBR_Framework.Models;
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
        internal static Dictionary<string, ST_MenuBaseInternal> DictionaryInternalMenuEvent = new Dictionary<string, ST_MenuBaseInternal>();
        //internal static Dictionary<string, string> DictionaryMenuDefault = new Dictionary<string, string>();
        internal static Dictionary<object, TableModel> DictionaryTablesFields = new Dictionary<object, TableModel>();
        internal static Dictionary<object, UdoModel> DictionaryUdos = new Dictionary<object, UdoModel>();
        internal static Dictionary<object, UdoChildsModel> DictionaryUdosChilds = new Dictionary<object, UdoChildsModel>();

        static private ST_B1AppDomain objAppDomainClass = null;
        static private SAPbouiCOM.Application objApplication = null;
        static private SAPbobsCOM.Company objCompany = null;
        static private string strConnectionString = "";
        static internal bool EventsView = false;

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
        static public string Port { get; set; }
        static public string UserDB { get; set; }
        static public string PasswdDB { get; set; }

        #endregion


        #region Interno

        internal static void RegisterMenuByType(string menuUid, ST_MenuBase menuBase)
        {
            if (!string.IsNullOrEmpty(menuUid))
                DictionaryMenuEvent.Add(menuUid, menuBase);
        }

        internal static void RegisterMenuInternalByType(string menuUid, ST_MenuBaseInternal menuBase)
        {
            if (!string.IsNullOrEmpty(menuUid))
                DictionaryInternalMenuEvent.Add(menuUid, menuBase);
        }

        internal static void RegisterFormByType(string formUid, ST_FormBase formBase)
        {
            if (!string.IsNullOrEmpty(formUid))
                DictionaryFormEvent.Add(formUid, formBase);
        }

        internal static void RegisterTable(object obj, TableModel tables)
        {
            if (obj != null)
                DictionaryTablesFields.Add(obj, tables);
        }

        internal static void RegisterUdo(object obj, UdoModel udo)
        {
            if (obj != null)
                DictionaryUdos.Add(obj, udo);
        }

        internal static void RegisterUdoChild(object obj, UdoChildsModel udo)
        {
            if (obj != null)
                DictionaryUdosChilds.Add(obj, udo);
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
                        //verifica se a classe possui o atributo de formulario
                        if (type.CustomAttributes.Where(e => e.AttributeType == typeof(ST_FormAttribute)).Count() > 0)
                            Activator.CreateInstance(type);

                        //verifica se a classe possui o atributo de menu
                        if (type.CustomAttributes.Where(e => e.AttributeType == typeof(ST_MenuAttribute)).Count() > 0)
                            Activator.CreateInstance(type);

                        

                    }
                    catch { }

                }
                else if (type.Name == "TablesAddon")
                    Activator.CreateInstance(type);

            }

        }


        #endregion
    }
}
