using SAPbouiCOM;
using System;

namespace STBR_Framework
{
    internal class GlobalEvents
    {
        //fields
        #region Menu Fields
        private static MenuEventHandler _MenuClick_Before;
        private static MenuEventHandler _MenuClick_After;
        #endregion


        //events
        #region Menu Events
        public static event MenuEventHandler MenuClick_Before;
        public static event MenuEventHandler MenuClick_After;
        #endregion


        //methods
        #region Captura Menu Events
        public static void CatchMenuEvent(ref MenuEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (pVal.BeforeAction)
                {
                    if (MenuClick_Before != null)
                        MenuClick_Before(ref pVal, ref bubbleEvent);
                }
                else
                {
                    if (MenuClick_After != null)
                        MenuClick_After(ref pVal, ref bubbleEvent);
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento de Menu:", ex);
            }
        }
        #endregion


        //delegates
        public delegate void MenuEventHandler(ref SAPbouiCOM.MenuEvent pVal, ref bool bubbleEvent);

    }
}
