namespace STBR_Framework.Utils
{
    public static class ST_Mensagens
    {
        public static void Box(string mensagem)
        {
            ST_B1AppDomain.Application.MessageBox(mensagem);
        }

        public static void StatusBar(string mensagem)
        {
            ST_B1AppDomain.Application.SetStatusBarMessage(mensagem, SAPbouiCOM.BoMessageTime.bmt_Short, false);
        }

        public static void StatusBarError(string mensagem)
        {
            ST_B1AppDomain.Application.StatusBar.SetText(mensagem, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
        }

        public static void StatusBarSuccess(string mensagem)
        {
            ST_B1AppDomain.Application.StatusBar.SetText(mensagem, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            //VZ_B1AppDomain.Application.StatusBar.SetSystemMessage(mensagem, 
            //    SAPbouiCOM.BoMessageTime.bmt_Short, 
            //    SAPbouiCOM.BoStatusBarMessageType.smt_Success, 
            //    MessageID: 999, 
            //    Context: "testando contexto", HelpContent: "testando help");
        }

        public static void StatusBarWarning(string mensagem)
        {
            ST_B1AppDomain.Application.StatusBar.SetText(mensagem, SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Warning);
        }
    }
}
