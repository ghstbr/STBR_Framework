using SAPbouiCOM;
using STBR_Framework.Utils;
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

        #region Item Fields
        private static ItemEventHandler _ItemEvent;
        private static ItemEventHandler _ItemPressed_Before;
        private static ItemEventHandler _ItemPressed_After;
        private static ItemEventHandler _ItemClicked_Before;
        private static ItemEventHandler _ItemClicked_After;
        private static ItemEventHandler _ChooseFromList_Before;
        private static ItemEventHandler _ChooseFromList_After;
        private static ItemEventHandler _ComboSelect_Before;
        private static ItemEventHandler _ComboSelect_After;
        private static ItemEventHandler _DoubleClick_Before;
        private static ItemEventHandler _DoubleClick_After;
        private static ItemEventHandler _FormActivate_Before;
        private static ItemEventHandler _FormActivate_After;
        private static ItemEventHandler _FormClose_Before;
        private static ItemEventHandler _FormClose_After;
        private static ItemEventHandler _FormKeyDown_Before;
        private static ItemEventHandler _FormKeyDown_After;
        private static ItemEventHandler _KeyDown_Before;
        private static ItemEventHandler _KeyDown_After;
        private static ItemEventHandler _FormDeactivate_Before;
        private static ItemEventHandler _FormDeactivate_After;
        private static ItemEventHandler _GotFocus_Before;
        private static ItemEventHandler _GotFocus_After;
        private static ItemEventHandler _LostFocus_Before;
        private static ItemEventHandler _LostFocus_After;
        private static ItemEventHandler _Form_Load_Before;
        private static ItemEventHandler _Form_Load_After;
        private static ItemEventHandler _Matrix_Link_Pressed_Before;
        private static ItemEventHandler _Matrix_Link_Pressed_After;
        #endregion

        #region RightClick Fields
        private static RightClickEventHandler _RightClick_Before;
        private static RightClickEventHandler _RightClick_After;
        #endregion

        #region Application Fields

        private static AppEventHandler _AppCompanyChanged;
        private static AppEventHandler _AppFontChanged;
        private static AppEventHandler _AppLanguageChanged;
        private static AppEventHandler _AppServerTerminition;
        private static AppEventHandler _AppShutdown;
        #endregion

        #region FormData Fields
        private static FormDataEventHandler _FormDataAdd_Before;
        private static FormDataEventHandler _FormDataAdd_After;
        private static FormDataEventHandler _FormDataDelete_Before;
        private static FormDataEventHandler _FormDataDelete_After;
        private static FormDataEventHandler _FormDataLoad_Before;
        private static FormDataEventHandler _FormDataLoad_After;
        private static FormDataEventHandler _FormDataUpdate_Before;
        private static FormDataEventHandler _FormDataUpdate_After;
        #endregion

        #region UDO Fields
        private static UdoEventHandler _UdoFormOpen;
        private static UdoEventHandler _UdoFormBuild;
        #endregion

        #region Print Fields

        private static PrintEventHandler _Print_Before;
        private static PrintEventHandler _Print_After;
        private static PrintEventHandler _Print_Layout_Before;
        private static PrintEventHandler _Print_Layout_After;

        #endregion

        #region Report Data

        private static ReportDataEventHandler _Print_Data_Before;
        private static ReportDataEventHandler _Print_Data_After;

        #endregion


        //events
        #region Menu Events
        public static event MenuEventHandler MenuClick_Before;
        public static event MenuEventHandler MenuClick_After;
        #endregion

        #region Item Events
        public static event ItemEventHandler ItemEvent;
        public static event ItemEventHandler ItemPressed_Before;
        public static event ItemEventHandler ItemPressed_After;
        public static event ItemEventHandler ItemClicked_Before;
        public static event ItemEventHandler ItemClicked_After;
        public static event ItemEventHandler ChooseFromList_Before;
        public static event ItemEventHandler ChooseFromList_After;
        public static event ItemEventHandler ComboSelect_Before;
        public static event ItemEventHandler ComboSelect_After;
        public static event ItemEventHandler DoubleClick_Before;
        public static event ItemEventHandler DoubleClick_After;
        public static event ItemEventHandler FormActivate_Before;
        public static event ItemEventHandler FormActivate_After;
        public static event ItemEventHandler FormClose_Before;
        public static event ItemEventHandler FormClose_After;
        public static event ItemEventHandler FormKeyDown_Before;
        public static event ItemEventHandler FormKeyDown_After;
        public static event ItemEventHandler KeyDown_Before;
        public static event ItemEventHandler KeyDown_After;
        public static event ItemEventHandler FormDeactivate_Before;
        public static event ItemEventHandler FormDeactivate_After;
        public static event ItemEventHandler GotFocus_Before;
        public static event ItemEventHandler GotFocus_After;
        public static event ItemEventHandler LostFocus_Before;
        public static event ItemEventHandler LostFocus_After;
        public static event ItemEventHandler Form_Unload_Before;
        public static event ItemEventHandler Form_Unload_After;
        public static event ItemEventHandler Form_Load_Before;
        public static event ItemEventHandler Form_Load_After;
        public static event ItemEventHandler Matrix_Link_Pressed_Before;
        public static event ItemEventHandler Matrix_Link_Pressed_After;
        public static event ItemEventHandler Form_Resize_Before;
        public static event ItemEventHandler Form_Resize_After;
        #endregion

        #region RightClick Events
        public static event RightClickEventHandler RightClick_Before;
        public static event RightClickEventHandler RightClick_After;
        #endregion

        #region ProgressBar Events
        public static event ProgressBarEventHandler ProgressBar_Created_Before;
        public static event ProgressBarEventHandler ProgressBar_Created_After;
        public static event ProgressBarEventHandler ProgressBar_Stopped_Before;
        public static event ProgressBarEventHandler ProgressBar_Stopped_After;
        public static event ProgressBarEventHandler ProgressBar_Released_Before;
        public static event ProgressBarEventHandler ProgressBar_Released_After;
        #endregion

        #region FormData Events
        public static event FormDataEventHandler FormDataAdd_Before;
        public static event FormDataEventHandler FormDataAdd_After;
        public static event FormDataEventHandler FormDataDelete_Before;
        public static event FormDataEventHandler FormDataDelete_After;
        public static event FormDataEventHandler FormDataUpdate_Before;
        public static event FormDataEventHandler FormDataUpdate_After;
        public static event FormDataEventHandler FormDataLoad_Before;
        public static event FormDataEventHandler FormDataLoad_After;
        #endregion

        #region Application Events
        public static event AppEventHandler AppCompanyChanged;
        public static event AppEventHandler AppLanguageChanged;
        public static event AppEventHandler AppServerTerminition;
        public static event AppEventHandler AppShutdown;
        public static event AppEventHandler AppFontChanged;
        #endregion

        #region UDO Events
        public static event UdoEventHandler UdoFormOpen;
        public static event UdoEventHandler UdoFormBuild;
        #endregion

        #region Print Events

        public static event PrintEventHandler Print_Before;
        public static event PrintEventHandler Print_After;
        public static event PrintEventHandler Print_Layout_Before;
        public static event PrintEventHandler Print_Layout_After;

        #endregion

        #region Report Data Events

        public static event ReportDataEventHandler Print_Data_Before;
        public static event ReportDataEventHandler Print_Data_After;

        #endregion


        //methods
        #region Captura Menu Events
        public static void CatchMenuEvent(ref MenuEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Menu Evento: MenuClick " +
                        " || Before: " + pVal.BeforeAction + " Id: " + pVal.MenuUID);

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

        #region Captura Item Events
        public static void CatchItemEvent(string formUID, ref ItemEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Item Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction + " || Form: " + pVal.FormTypeEx);


                if (pVal != null)
                {

                    switch (pVal.EventType)
                    {
                        
                        case BoEventTypes.et_ITEM_PRESSED:
                            if (pVal.BeforeAction)
                            {
                                if (ItemPressed_Before != null)
                                    ItemPressed_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ItemPressed_After != null)
                                    ItemPressed_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_LOAD:
                            if (pVal.BeforeAction)
                            {
                                if (Form_Load_Before != null)
                                    Form_Load_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Form_Load_After != null)
                                    Form_Load_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_UNLOAD:
                            if (pVal.BeforeAction)
                            {
                                if (Form_Unload_Before != null)
                                    Form_Unload_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Form_Unload_After != null)
                                    Form_Unload_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_CLICK:
                            if (pVal.BeforeAction)
                            {
                                if (ItemClicked_Before != null)
                                    ItemClicked_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ItemClicked_After != null)
                                    ItemClicked_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_CHOOSE_FROM_LIST:
                            if (pVal.BeforeAction)
                            {
                                if (ChooseFromList_Before != null)
                                    ChooseFromList_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ChooseFromList_After != null)
                                    ChooseFromList_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_COMBO_SELECT:
                            if (pVal.BeforeAction)
                            {
                                if (ComboSelect_Before != null)
                                    ComboSelect_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ComboSelect_After != null)
                                    ComboSelect_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_DOUBLE_CLICK:
                            if (pVal.BeforeAction)
                            {
                                if (DoubleClick_Before != null)
                                    DoubleClick_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (DoubleClick_After != null)
                                    DoubleClick_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_ACTIVATE:
                            if (pVal.BeforeAction)
                            {
                                if (FormActivate_Before != null)
                                    FormActivate_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (FormActivate_After != null)
                                    FormActivate_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_CLOSE:
                            if (pVal.BeforeAction)
                            {
                                if (FormClose_Before != null)
                                    FormClose_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (FormClose_After != null)
                                    FormClose_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_KEY_DOWN:
                            if (pVal.BeforeAction)
                            {
                                if (FormKeyDown_Before != null)
                                    FormKeyDown_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (FormKeyDown_After != null)
                                    FormKeyDown_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_KEY_DOWN:
                            if (pVal.BeforeAction)
                            {
                                if (KeyDown_Before != null)
                                    KeyDown_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (KeyDown_After != null)
                                    KeyDown_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_DEACTIVATE:
                            if (pVal.BeforeAction)
                            {
                                if (FormDeactivate_Before != null)
                                    FormDeactivate_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (FormDeactivate_After != null)
                                    FormDeactivate_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_GOT_FOCUS:
                            if (pVal.BeforeAction && pVal.ActionSuccess)
                            {
                                if (GotFocus_Before != null)
                                    GotFocus_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (GotFocus_After != null)
                                    GotFocus_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_LOST_FOCUS:
                            if (pVal.BeforeAction && pVal.ActionSuccess)
                            {
                                if (LostFocus_Before != null)
                                    LostFocus_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (LostFocus_After != null)
                                    LostFocus_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_MATRIX_LINK_PRESSED:
                            if (pVal.BeforeAction)
                            {
                                if (Matrix_Link_Pressed_Before != null)
                                    Matrix_Link_Pressed_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Matrix_Link_Pressed_After != null)
                                    Matrix_Link_Pressed_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoEventTypes.et_FORM_RESIZE:
                            if (pVal.BeforeAction)
                            {
                                if (Form_Resize_Before != null)
                                    Form_Resize_Before(formUID, ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Form_Resize_After != null)
                                    Form_Resize_After(formUID, ref pVal, ref bubbleEvent);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento Item: ", ex);
            }
        }
        #endregion

        #region Captura ProgressBar Events
        public static void CatchProgressBarEvent(ref ProgressBarEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Progress Bar Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction);

                if (pVal != null)
                {
                    switch (pVal.EventType)
                    {
                        case BoProgressBarEventTypes.pbet_ProgressBarCreated:
                            if (pVal.BeforeAction)
                            {
                                if (ProgressBar_Created_Before != null)
                                    ProgressBar_Created_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ProgressBar_Created_After != null)
                                    ProgressBar_Created_After(ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoProgressBarEventTypes.pbet_ProgressBarStopped:
                            if (pVal.BeforeAction)
                            {
                                if (ProgressBar_Stopped_Before != null)
                                    ProgressBar_Stopped_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ProgressBar_Stopped_After != null)
                                    ProgressBar_Stopped_After(ref pVal, ref bubbleEvent);
                            }
                            break;

                        case BoProgressBarEventTypes.pbet_ProgressBarReleased:
                            if (pVal.BeforeAction)
                            {
                                if (ProgressBar_Released_Before != null)
                                    ProgressBar_Released_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (ProgressBar_Released_After != null)
                                    ProgressBar_Released_After(ref pVal, ref bubbleEvent);
                            }
                            break;
                    }
                }


            }
            catch (Exception ex)
            {

                ST_B1Exception.throwException("Erro evento Progress Bar ::", ex);
            }

        }
        #endregion       

        #region Captura RightClick Events
        public static void CatchRightClickEvent(ref ContextMenuInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Right Click Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction);

                if (pVal.BeforeAction)
                {
                    if (RightClick_Before != null)
                        RightClick_Before(ref pVal, ref bubbleEvent);
                }
                else
                {
                    if (RightClick_After != null)
                        RightClick_After(ref pVal, ref bubbleEvent);
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento RightClick:", ex);
            }
        }
        #endregion

        #region Captura UDO Events
        public static void CatchUdoEvent(ref UDOEvent pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("UDO Evento: " + pVal.EventType);

                switch (pVal.EventType)
                {
                    case BoEventTypes.et_UDO_FORM_OPEN:
                        if (UdoFormOpen != null)
                            UdoFormOpen(ref pVal, ref bubbleEvent);
                        break;

                    case BoEventTypes.et_UDO_FORM_BUILD:
                        if (UdoFormBuild != null)
                            UdoFormBuild(ref pVal, ref bubbleEvent);
                        break;
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento UDO:", ex);
            }
        }
        #endregion

        #region Captura FormData Events
        public static void CatchFormDataEvent(ref BusinessObjectInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;
            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Form Data Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction);

                switch (pVal.EventType)
                {
                    case BoEventTypes.et_FORM_DATA_ADD:
                        if (pVal.BeforeAction)
                        {
                            if (FormDataAdd_Before != null)
                                FormDataAdd_Before(ref pVal, ref bubbleEvent);
                        }
                        else
                        {
                            if (FormDataAdd_After != null)
                                FormDataAdd_After(ref pVal, ref bubbleEvent);
                        }
                        break;

                    case BoEventTypes.et_FORM_DATA_DELETE:
                        if (pVal.BeforeAction)
                        {
                            if (FormDataDelete_Before != null)
                                FormDataDelete_Before(ref pVal, ref bubbleEvent);
                        }
                        else
                        {
                            if (FormDataDelete_After != null)
                                FormDataDelete_After(ref pVal, ref bubbleEvent);
                        }
                        break;

                    case BoEventTypes.et_FORM_DATA_LOAD:
                        if (pVal.BeforeAction)
                        {
                            if (FormDataLoad_Before != null)
                                FormDataLoad_Before(ref pVal, ref bubbleEvent);
                        }
                        else
                        {
                            if (FormDataLoad_After != null)
                                FormDataLoad_After(ref pVal, ref bubbleEvent);
                        }
                        break;

                    case BoEventTypes.et_FORM_DATA_UPDATE:
                        if (pVal.BeforeAction)
                        {
                            if (FormDataUpdate_Before != null)
                                FormDataUpdate_Before(ref pVal, ref bubbleEvent);
                        }
                        else
                        {
                            if (FormDataUpdate_After != null)
                                FormDataUpdate_After(ref pVal, ref bubbleEvent);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro de evento FormData:", ex);
            }
        }

        #endregion

        #region Captura Application Events
        public static void CatchAppEvents(BoAppEventTypes pVal)
        {
            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("App Evento: " + pVal);

                switch (pVal)
                {
                    case BoAppEventTypes.aet_CompanyChanged:
                        if (AppCompanyChanged != null)
                            AppCompanyChanged(ref pVal);
                        break;

                    case BoAppEventTypes.aet_FontChanged:
                        if (AppFontChanged != null)
                            AppFontChanged(ref pVal);
                        break;

                    case BoAppEventTypes.aet_LanguageChanged:
                        if (AppLanguageChanged != null)
                            AppLanguageChanged(ref pVal);
                        break;

                    case BoAppEventTypes.aet_ServerTerminition:
                        if (AppServerTerminition != null)
                            AppServerTerminition(ref pVal);
                        break;

                    case BoAppEventTypes.aet_ShutDown:
                        if (AppShutdown != null)
                            AppShutdown(ref pVal);
                        break;
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento App:", ex);
            }
        }
        #endregion

        #region Captura Report Data

        public static void CatchReportDataEvent(ref ReportDataInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Report Data Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction);

                if (pVal != null)
                {
                    switch (pVal.EventType)
                    {
                        case BoEventTypes.et_PRINT_DATA:
                            if (pVal.BeforeAction)
                            {
                                if (Print_Data_Before != null)
                                    Print_Data_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Print_Data_After != null)
                                    Print_Data_After(ref pVal, ref bubbleEvent);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento Report: ", ex);
            }

        }

        #endregion

        #region Captura Print Events
        public static void CatchPrintEvent(ref PrintEventInfo pVal, out bool bubbleEvent)
        {
            bubbleEvent = true;

            try
            {
                if (ST_B1AppDomain.EventsView)
                    ST_Mensagens.StatusBar("Print Evento: " + pVal.EventType +
                        " || Before: " + pVal.BeforeAction);

                if (pVal != null)
                {

                    switch (pVal.EventType)
                    {
                        case BoEventTypes.et_PRINT:
                            if (pVal.BeforeAction)
                            {
                                if (Print_Before != null)
                                    Print_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Print_After != null)
                                    Print_After(ref pVal, ref bubbleEvent);
                            }
                            break;



                        case BoEventTypes.et_PRINT_LAYOUT_KEY:
                            if (pVal.BeforeAction)
                            {
                                if (Print_Layout_Before != null)
                                    Print_Layout_Before(ref pVal, ref bubbleEvent);
                            }
                            else
                            {
                                if (Print_Layout_After != null)
                                    Print_Layout_After(ref pVal, ref bubbleEvent);
                            }
                            break;


                    }
                }
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Erro evento Print: ", ex);
            }
        }


        #endregion

        

        //delegates
        public delegate void MenuEventHandler(ref MenuEvent pVal, ref bool bubbleEvent);
        public delegate void ItemEventHandler(string formUID, ref ItemEvent pVal, ref bool bubbleEvent);
        public delegate void ProgressBarEventHandler(ref ProgressBarEvent pVal, ref bool bubbleEvent);
        public delegate void RightClickEventHandler(ref ContextMenuInfo pVal, ref bool bubbleEvent);
        public delegate void FormDataEventHandler(ref BusinessObjectInfo pVal, ref bool bubbleEvent);
        public delegate void UdoEventHandler(ref UDOEvent pVal, ref bool bubbleEvent);
        public delegate void AppEventHandler(ref BoAppEventTypes pVal);
        public delegate void PrintEventHandler(ref PrintEventInfo pVal, ref bool bubbleEvent);
        public delegate void ReportDataEventHandler(ref ReportDataInfo pVal, ref bool bubbleEvent);
    }
}
