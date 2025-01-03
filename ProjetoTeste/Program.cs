using STBR_Framework;
using STBR_Framework.Connections;
using System;
using Application = SAPbouiCOM.Framework.Application;

namespace ProjetoTeste
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application oApp = null;
                if (args.Length < 1)
                {
                    oApp = new Application();
                }
                else
                {
                   
                    oApp = new Application(args[0]);
                }


                ConnectSDK conn = new ConnectSDK();
                ConnectSDK._application = oApp;
                conn.Connect(types: typeof(Program).Namespace.ST_GetTypesFromAssembly(), autoCreateDB: false);
                oApp.Run();
                //codigo que precisar daqui para baixo


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
