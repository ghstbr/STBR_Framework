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
