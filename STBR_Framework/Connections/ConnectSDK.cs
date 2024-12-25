using SAPbouiCOM.Framework;
using System;

namespace STBR_Framework.Connections
{
    public class ConnectSDK
    {
        public static Application _application;
        private static Type[] _types;
        public void Connect(string[] args = null, Type[] types = null)
        {
            try
            {
                //if (args == null)
                //    _application = new Application();
                //else if (args.Length < 1)
                //    _application = new Application();
                //else
                //    _application = new Application(args[0]);

                _types = types;

                _application.AfterInitialized += ApplicationRun;
                Application.SBO_Application.AppEvent += SBO_Application_AppEvent;
                //_application.Run();
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
            new Events();
            if (_types != null)
                ST_B1AppDomain.CreateInstanceClass(_types);
                        
            
            ST_B1AppDomain.Application.SetStatusBarMessage("Conexão estabelecida com sucesso!", SAPbouiCOM.BoMessageTime.bmt_Short, false);
        }


    }
}
