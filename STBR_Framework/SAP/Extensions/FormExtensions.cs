using SAPbouiCOM;
using System;
using System.Threading;
using System.Windows.Forms;

namespace STBR_Framework
{
    public static class FormExtensions
    {
        private static string Path = "";
        private static string Message = "";
        private static string FileName = "";


        private static void SelectPath()
        {
            WindowWrapper owner = new WindowWrapper();
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = Message,
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false
            };
            dialog.ShowDialog(owner);
            Path = dialog.SelectedPath;
        }

        private static void SelectPathFile()
        {
            WindowWrapper owner = new WindowWrapper();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog(owner);
            Path = dialog.FileName;
            FileName = dialog.SafeFileName;
        }

        /// <summary>
        /// Abre janela para seleção de pasta e preenche o caminho no campo informado
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="message">Mensagem para aparecer na hora da selecao de pasta</param>
        /// <param name="fieldName">campo da tela no SAP para preencher com o caminho</param>
        public static void ST_FolderDialog(this SAPbouiCOM.Form oForm, string message, string fieldName)
        {
            try
            {
                Message = message;
                Thread thread = null;

                thread = new Thread(new ThreadStart(SelectPath));
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else if (thread.ThreadState == ThreadState.Stopped)
                {
                    thread.Start();
                    thread.Join();
                }
                while (thread.ThreadState == ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                ((EditText)oForm.Items.Item(fieldName).Specific).Value = Path;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao selecionar caminho da pasta :: ", ex);
            }
        }

        /// <summary>
        /// Abre janela para seleção de pasta e retorna string com o caminho selecionado
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="message">Mensagem para aparecer na hora da selecao de pasta</param>
        /// <returns></returns>
        public static string ST_FolderDialog(this System.Windows.Forms.Form oForm, string message)
        {
            try
            {
                Message = message;
                Thread thread = null;

                thread = new Thread(new ThreadStart(SelectPath));
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else if (thread.ThreadState == ThreadState.Stopped)
                {
                    thread.Start();
                    thread.Join();
                }
                while (thread.ThreadState == ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                return Path;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao selecionar caminho da pasta :: ", ex);
                return "";
            }
        }

        /// <summary>
        /// Abre janela para seleção de arquivo e preenche o caminho no campo informado
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="message">Mensagem para aparecer na hora da selecao de arquivo</param>
        /// <param name="fieldName">Campo da tela do SAP para preecher com o caminho</param>
        public static void ST_FileDialog(this SAPbouiCOM.Form oForm, string message, string fieldName)
        {
            try
            {
                Message = message;
                Thread thread = null;

                thread = new Thread(new ThreadStart(SelectPathFile));
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else if (thread.ThreadState == ThreadState.Stopped)
                {
                    thread.Start();
                    thread.Join();
                }
                while (thread.ThreadState == ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                ((EditText)oForm.Items.Item(fieldName).Specific).Value = Path;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao selecionar caminho do arquivo :: ", ex);
            }
        }

        /// <summary>
        /// Abre janela para seleção de arquivo e retorna uma string com o caminho e outra com o nome do arquivo
        /// </summary>
        /// <param name="oForm"></param>
        /// <param name="message">Mensagem para aparecer na hora da selecao de arquivo</param>
        /// <param name="path">Caminho do arquivo selecionado</param>
        /// <param name="fileName">Nome do arquivo selecionado</param>
        public static void ST_FileDialog(this SAPbouiCOM.Form oForm, string message, out string path, out string fileName)
        {
            Message = message;
            Thread thread = null;
            Path = "";
            FileName = "";
            try
            {
                thread = new Thread(new ThreadStart(SelectPathFile));
                if (thread.ThreadState == ThreadState.Unstarted)
                {
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                }
                else if (thread.ThreadState == ThreadState.Stopped)
                {
                    thread.Start();
                    thread.Join();
                }
                while (thread.ThreadState == ThreadState.Running)
                {
                    System.Windows.Forms.Application.DoEvents();
                }
                path = Path;
                fileName = FileName;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao selecionar caminho do arquivo :: ", ex);
                path = "";
                fileName = "";
            }
        }



        public static void FreezeForm(this SAPbouiCOM.Form oForm, bool active = true)
        {
            try
            {
                if (active)
                {
                    oForm.Freeze(true);
                }
                else
                {
                    oForm.Freeze(false);
                }


            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro ao carregar formulario :: ", ex);
            }
        }



    }
}
