using System;
using System.IO;

namespace STBR_Framework
{
    /// <summary>
    /// Classe para tratamento de execoes e criacao automatica de logs
    /// os arquivos de logs ficarao salvos na pasta do projeto em execucao
    /// </summary>
    public sealed class ST_B1Exception
    {
        static private ST_B1Exception objException = null;
        private ST_B1Exception()
        {
        }
        static void SaveFile(string strMessage, string strDate, string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }


            using (StreamWriter sw = new StreamWriter(strPath + "\\LOG-" + strDate + ".log", true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString());
                sw.WriteLine(strMessage);
            }
        }

        /// <summary>
        /// Salva a mensagem no arquivo de log
        /// </summary>
        /// <param name="strAssembly">Namespace em execucao</param>
        /// <param name="er">exeption</param>
        static public void throwException(string strAssembly, Exception er)
        {

            string strErrorMessage = "";

            if (objException == null)

                objException = new ST_B1Exception();

            try
            {
                if (er != null && er.Message.Length > 0)
                {
                    System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(er, true);
                    string strGetAssemblyInfo = "Method:" + trace.GetFrame(0).GetMethod().Name + " Line: " + trace.GetFrame(0).GetFileLineNumber() + " Column: " + trace.GetFrame(0).GetFileColumnNumber();
                    strErrorMessage = strAssembly + "-" + strGetAssemblyInfo + " ::: " + er.Message;
                }
                else
                {
                    strErrorMessage = strAssembly;
                }

                writeLog(strErrorMessage);



            }

            catch (Exception objEx)
            {
                writeLog("ExceptionClass:" + strAssembly + ": " + objEx.Message);

            }



        }

        /// <summary>
        /// Salva a mensagem do codigo de erro do SAP passado
        /// </summary>
        /// <param name="codErrorSap">codigo de erro do SAP</param>
        static public void throwException(int codErrorSap)
        {
            string strErrorMessage = "";

            if (objException == null)

                objException = new ST_B1Exception();

            try
            {

                if (codErrorSap != 0)
                {
                    string mensagem = "";
                    ST_B1AppDomain.Company.GetLastError(out codErrorSap, out mensagem);
                    strErrorMessage = codErrorSap + " :: " + mensagem;

                    writeLog(strErrorMessage);
                }



            }
            catch (Exception objEx)
            {

                writeLog("ExceptionClass:" + ": " + objEx.Message);
            }


        }

        /// <summary>
        /// Salva a mensagem do codigo de erro do SAP passado
        /// </summary>
        /// <param name="codErrorSap">codigo de erro do SAP</param>
        /// <param name="complementMessage">mensagem complementar</param>
        static public void throwException(int codErrorSap, string complementMessage)
        {
            string strErrorMessage = "";

            if (objException == null)

                objException = new ST_B1Exception();

            try
            {

                if (codErrorSap != 0)
                {
                    string mensagem = "";
                    ST_B1AppDomain.Company.GetLastError(out codErrorSap, out mensagem);
                    strErrorMessage = codErrorSap + " :: " + mensagem;

                    writeLog(strErrorMessage + " :: " + complementMessage);
                }



            }
            catch (Exception objEx)
            {

                writeLog("ExceptionClass:" + ": " + objEx.Message);
            }


        }

        /// <summary>
        /// Salva no arquivo de log a mensagem passada
        /// </summary>
        /// <param name="strMessage">mensagem a ser salva</param>
        static public void writeLog(string strMessage)
        {

            string strDate = DateTime.Now.ToString("yyyyMMdd");
            string strPath = AppDomain.CurrentDomain.BaseDirectory + "\\LOGS\\";

            try
            {
                ST_B1AppDomain.Application.SetStatusBarMessage(strMessage);

                SaveFile(strMessage, strDate, strPath);

            }

            catch (Exception er)
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(er, true);
                string strGetAssemblyInfo = trace.GetFrame(0).GetMethod().Name + " Line: " + trace.GetFrame(0).GetFileLineNumber() + " Column: " + trace.GetFrame(0).GetFileColumnNumber();
                string strErrorMessage = er.Message + " ::: " + strGetAssemblyInfo;
                SaveFile(strMessage, strDate, strPath);
            }


        }
    }
}
