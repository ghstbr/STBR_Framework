using System;
using SAPbouiCOM;

namespace STBR_Framework
{
    internal class Events
    {
        static private SAPbouiCOM.Application _objApplication = null;
        public Events()
        {
            try
            {
                _objApplication = ST_B1AppDomain.Application;


                _objApplication.MenuEvent += new _IApplicationEvents_MenuEventEventHandler(GlobalEvents.CatchMenuEvent);
                _objApplication.AppEvent += new _IApplicationEvents_AppEventEventHandler(GlobalEvents.CatchAppEvents);
                _objApplication.ItemEvent += new _IApplicationEvents_ItemEventEventHandler(GlobalEvents.CatchItemEvent);
                _objApplication.RightClickEvent += new _IApplicationEvents_RightClickEventEventHandler(GlobalEvents.CatchRightClickEvent);
                _objApplication.FormDataEvent += new _IApplicationEvents_FormDataEventEventHandler(GlobalEvents.CatchFormDataEvent);
                _objApplication.UDOEvent += new _IApplicationEvents_UDOEventEventHandler(GlobalEvents.CatchUdoEvent);
                _objApplication.ProgressBarEvent += new _IApplicationEvents_ProgressBarEventEventHandler(GlobalEvents.CatchProgressBarEvent);
                _objApplication.PrintEvent += new _IApplicationEvents_PrintEventEventHandler(GlobalEvents.CatchPrintEvent);
                _objApplication.ReportDataEvent += new _IApplicationEvents_ReportDataEventEventHandler(GlobalEvents.CatchReportDataEvent);

                _objApplication.MetadataAutoRefresh = true;
                ST_B1AppDomain.Application = _objApplication;
            }
            catch (Exception ex)
            {
                ST_B1Exception.throwException("Eventos: ", ex);
            }
        }

    }
}
