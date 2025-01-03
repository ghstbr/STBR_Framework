using SAPbouiCOM.Framework;
using STBR_Framework.InternalSystem.Menu;
using STBR_Framework.InternalSystem.Services;
using STBR_Framework.SAP.Database;
using System;

namespace STBR_Framework.Connections
{
    public class ConnectSDK
    {
        public static Application _application;
        private static Type[] _types;
        private static bool _autoCreateDB;
        private static string _userDB;
        private static string _passwdDB;
        private static string _port;

        public void Connect(string[] args = null, 
            Type[] types = null, 
            bool autoCreateDB = true,
            string userDB = "",
            string passwdDB = "",
            string port = "")
        {
            try
            {
                _autoCreateDB = autoCreateDB;
                _userDB = userDB;
                _passwdDB = passwdDB;
                _port = port;
                _types = types;

                _application.AfterInitialized += ApplicationRun;
                Application.SBO_Application.AppEvent += SBO_Application_AppEvent;
                
            }
            catch (Exception ex)
            {
                ST_B1Exception.writeLog(ex.Message);
            }
        }

        private void SBO_Application_AppEvent(SAPbouiCOM.BoAppEventTypes EventType)
        {
            switch (EventType)
            {
                case SAPbouiCOM.BoAppEventTypes.aet_ShutDown:
                    //Exit Add-On
                    System.Windows.Forms.Application.Exit();
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_CompanyChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_FontChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_LanguageChanged:
                    break;
                case SAPbouiCOM.BoAppEventTypes.aet_ServerTerminition:
                    break;
                default:
                    break;
            }
        }

        private static void ApplicationRun(object sender, EventArgs e)
        {
            ST_B1AppDomain.Application = Application.SBO_Application;
            ST_B1AppDomain.Company = (SAPbobsCOM.Company)Application.SBO_Application.Company.GetDICompany();
            ST_B1AppDomain.Connected = true;
            ST_B1AppDomain.Port = _port;
            ST_B1AppDomain.UserDB = _userDB;
            ST_B1AppDomain.PasswdDB = _passwdDB;
            new Events();
            if (_types != null)
                ST_B1AppDomain.CreateInstanceClass(_types);

            new MenuFramework();
            InitForms_service.Start();
            if (_autoCreateDB)
                new DB();

            ST_B1AppDomain.Application.SetStatusBarMessage("Conexão estabelecida com sucesso! " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), SAPbouiCOM.BoMessageTime.bmt_Short, false);
        }


    }
}
